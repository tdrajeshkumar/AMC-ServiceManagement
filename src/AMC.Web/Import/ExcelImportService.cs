using AMC.Web.Data;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace AMC.Web.Import;

public class ExcelImportService
{
    private readonly ApplicationDbContext _db;
    private readonly WorkbookAnalyzer _analyzer;
    private readonly ImportValidationService _validation;
    private readonly CustomerImportService _customers;
    private readonly ContractImportService _contracts;

    public ExcelImportService(
        ApplicationDbContext db,
        WorkbookAnalyzer analyzer,
        ImportValidationService validation,
        CustomerImportService customers,
        ContractImportService contracts)
    {
        _db = db;
        _analyzer = analyzer;
        _validation = validation;
        _customers = customers;
        _contracts = contracts;
    }

    public async Task<ImportResult> ImportAsync(string? workbookPath = null, CancellationToken cancellationToken = default)
    {
        var result = new ImportResult();
        var path = ResolveWorkbookPath(workbookPath);

        var analysis = _analyzer.Analyze(path);
        var stagingContracts = new List<StagingContractRow>();
        var stagingBillings = new List<StagingBillingRow>();
        var stagingPms = new List<StagingPmRow>();

        using var fs = File.OpenRead(path);
        IWorkbook workbook = new XSSFWorkbook(fs);

        foreach (var sheetInfo in analysis.Sheets.Where(s => s.IsAnnualPlanningSheet))
        {
            var sheet = workbook.GetSheet(sheetInfo.SheetName);
            ParseAnnualSheet(sheet, sheetInfo, stagingContracts, stagingBillings, stagingPms);
        }

        result.TotalRows = stagingContracts.Count + stagingBillings.Count + stagingPms.Count;

        var issues = await _validation.ValidateAsync(stagingContracts, stagingBillings, stagingPms, cancellationToken);
        result.Warnings += issues.Count(i => !i.Contains("Invalid", StringComparison.OrdinalIgnoreCase));
        result.Errors += issues.Count(i => i.Contains("Invalid", StringComparison.OrdinalIgnoreCase));
        result.Messages.AddRange(issues);

        var (custInserted, custUpdated, customerMap) = await _customers.UpsertCustomersAsync(stagingContracts, cancellationToken);
        var (contractInserted, contractUpdated) = await _contracts.ImportContractsAsync(stagingContracts, customerMap, cancellationToken);

        result.Inserted = custInserted + contractInserted;
        result.Updated = custUpdated + contractUpdated;
        result.Skipped = Math.Max(0, result.TotalRows - (result.Inserted + result.Updated));

        result.Messages.Add($"Workbook remarks preserved in staging objects: {stagingContracts.Count(x => !string.IsNullOrWhiteSpace(x.RemarksRaw))} contract rows, {stagingBillings.Count(x => !string.IsNullOrWhiteSpace(x.RemarksRaw))} billing rows, {stagingPms.Count(x => !string.IsNullOrWhiteSpace(x.RemarksRaw))} PM rows.");

        return result;
    }

    private static void ParseAnnualSheet(
        ISheet sheet,
        SheetAnalysis info,
        List<StagingContractRow> contracts,
        List<StagingBillingRow> billings,
        List<StagingPmRow> pms)
    {
        var headerMap = BuildHeaderMap(sheet.GetRow(info.HeaderRow - 1));

        string? currentCustomer = null;
        for (int r = info.HeaderRow; r <= sheet.LastRowNum; r++)
        {
            var row = sheet.GetRow(r);
            if (row == null) continue;

            var customer = Cell(row, headerMap, "CLIENT");
            if (!string.IsNullOrWhiteSpace(customer)) currentCustomer = customer;

            var remarks = Cell(row, headerMap, "Remarks") ?? Cell(row, headerMap, "Remark");
            var pmCycle = Cell(row, headerMap, "PM Cycle");
            var pmSchedule = Cell(row, headerMap, "PM Schedule");
            var pmMonth = Cell(row, headerMap, "PM Month");
            var period = Cell(row, headerMap, "Period");
            var product = Cell(row, headerMap, "PRODUCT");
            var totalAmount = ParseDecimal(Cell(row, headerMap, "TOTAL AMOUNT"));

            if (!string.IsNullOrWhiteSpace(currentCustomer) && (!string.IsNullOrWhiteSpace(product) || totalAmount.HasValue))
            {
                contracts.Add(new StagingContractRow
                {
                    SourceSheet = sheet.SheetName,
                    SourceRow = r + 1,
                    CustomerName = currentCustomer,
                    ProductCovered = product,
                    TotalAmount = totalAmount,
                    PeriodText = period,
                    PmCycle = pmCycle,
                    PmSchedule = pmSchedule,
                    RemarksRaw = remarks
                });
            }

            if (!string.IsNullOrWhiteSpace(currentCustomer) && !string.IsNullOrWhiteSpace(pmMonth))
            {
                pms.Add(new StagingPmRow
                {
                    SourceSheet = sheet.SheetName,
                    SourceRow = r + 1,
                    CustomerName = currentCustomer,
                    PmCycle = pmCycle,
                    PmMonth = pmMonth,
                    RemarksRaw = remarks
                });
            }

            var billed = ParseDecimal(Cell(row, headerMap, "Billed"));
            if (!string.IsNullOrWhiteSpace(currentCustomer) && billed.HasValue)
            {
                billings.Add(new StagingBillingRow
                {
                    SourceSheet = sheet.SheetName,
                    SourceRow = r + 1,
                    CustomerName = currentCustomer,
                    PlannedAmount = billed,
                    RemarksRaw = remarks
                });
            }
        }
    }

    private static Dictionary<string, int> BuildHeaderMap(IRow? headerRow)
    {
        var map = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        if (headerRow == null) return map;
        for (int c = headerRow.FirstCellNum; c < headerRow.LastCellNum; c++)
        {
            var txt = headerRow.GetCell(c)?.ToString()?.Trim();
            if (!string.IsNullOrWhiteSpace(txt) && !map.ContainsKey(txt)) map[txt] = c;
        }
        return map;
    }

    private static string? Cell(IRow row, Dictionary<string, int> headerMap, string header)
        => headerMap.TryGetValue(header, out var c) ? row.GetCell(c)?.ToString()?.Trim() : null;

    private static decimal? ParseDecimal(string? value)
        => decimal.TryParse(value, out var d) ? d : null;

    private static string ResolveWorkbookPath(string? workbookPath)
    {
        if (!string.IsNullOrWhiteSpace(workbookPath) && File.Exists(workbookPath)) return workbookPath;
        if (File.Exists(ImportConstants.PrimaryWorkbookPath)) return ImportConstants.PrimaryWorkbookPath;
        if (File.Exists(ImportConstants.FallbackWorkbookPath)) return ImportConstants.FallbackWorkbookPath;
        throw new FileNotFoundException("Workbook not found.");
    }
}
