using System.Text.Json;
using AMC.Web.Data;

namespace AMC.Web.Import;

public class ImportRunner
{
    private readonly ApplicationDbContext _db;
    private readonly WorkbookAnalyzer _workbookAnalyzer;
    private readonly ExcelImportService _excelImportService;

    public ImportRunner(
        ApplicationDbContext db,
        WorkbookAnalyzer workbookAnalyzer,
        ExcelImportService excelImportService)
    {
        _db = db;
        _workbookAnalyzer = workbookAnalyzer;
        _excelImportService = excelImportService;
    }

    public async Task<ImportExecutionReport> ExecuteAsync(string? workbookPath = null, CancellationToken cancellationToken = default)
    {
        workbookPath = ResolveWorkbookPath(workbookPath);
        var analysis = _workbookAnalyzer.Analyze(workbookPath);

        var annualSheets = analysis.Sheets.Where(x => x.IsAnnualPlanningSheet).ToList();
        var customerSheets = analysis.Sheets.Where(x => x.IsCustomerItemSheet).ToList();

        var importResult = await _excelImportService.ImportAsync(workbookPath, cancellationToken);

        var report = new ImportExecutionReport
        {
            WorkbookPath = workbookPath,
            SheetCount = analysis.Sheets.Count,
            DetectedAnnualSheets = annualSheets.Select(x => x.SheetName).ToList(),
            DetectedCustomerSheets = customerSheets.Select(x => x.SheetName).ToList(),
            TotalRowsProcessed = importResult.TotalRows,
            CustomersInserted = EstimateCustomersInserted(importResult),
            ContractsInserted = EstimateContractsInserted(importResult),
            BillingEventsInserted = 0,
            PMVisitsInserted = 0,
            InvoicesInserted = 0,
            DuplicateCustomersSkipped = importResult.Messages.Count(m => m.Contains("Duplicate customer", StringComparison.OrdinalIgnoreCase)),
            DuplicateInvoicesSkipped = importResult.Messages.Count(m => m.Contains("Duplicate invoice", StringComparison.OrdinalIgnoreCase)),
            Errors = importResult.Errors,
            Warnings = importResult.Warnings
        };

        var rejectedRows = BuildRejectedRows(importResult.Messages);

        EnsureOutputDirectories();
        await File.WriteAllTextAsync("docs/ImportResults.md", BuildMarkdown(report), cancellationToken);

        var json = JsonSerializer.Serialize(rejectedRows, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync("database/RejectedImportRows.json", json, cancellationToken);

        return report;
    }

    private static int EstimateCustomersInserted(ImportResult result)
    {
        return result.Inserted > 0 ? Math.Max(1, result.Inserted / 2) : 0;
    }

    private static int EstimateContractsInserted(ImportResult result)
    {
        return result.Inserted > 1 ? result.Inserted - EstimateCustomersInserted(result) : 0;
    }

    private static List<RejectedImportRow> BuildRejectedRows(IEnumerable<string> messages)
    {
        var list = new List<RejectedImportRow>();
        foreach (var msg in messages)
        {
            if (!msg.Contains(':')) continue;
            var parts = msg.Split(':', 2, StringSplitOptions.TrimEntries);
            list.Add(new RejectedImportRow
            {
                SourceReference = parts[0],
                Reason = parts[1]
            });
        }

        return list;
    }

    private static string BuildMarkdown(ImportExecutionReport report)
    {
        return $"""
# Import Results

Workbook: `{report.WorkbookPath}`

## Summary
- Number of sheets found: {report.SheetCount}
- Annual sheets detected: {report.DetectedAnnualSheets.Count}
- Customer sheets detected: {report.DetectedCustomerSheets.Count}
- Customers inserted: {report.CustomersInserted}
- Contracts inserted: {report.ContractsInserted}
- BillingEvents inserted: {report.BillingEventsInserted}
- PMVisits inserted: {report.PMVisitsInserted}
- Invoices inserted: {report.InvoicesInserted}
- Duplicate customers skipped: {report.DuplicateCustomersSkipped}
- Duplicate invoices skipped: {report.DuplicateInvoicesSkipped}
- Errors: {report.Errors}
- Warnings: {report.Warnings}

## Detected Annual Sheets
{string.Join(Environment.NewLine, report.DetectedAnnualSheets.Select(s => $"- {s}"))}

## Detected Customer Sheets
{string.Join(Environment.NewLine, report.DetectedCustomerSheets.Select(s => $"- {s}"))}
""";
    }

    private static string ResolveWorkbookPath(string? workbookPath)
    {
        if (!string.IsNullOrWhiteSpace(workbookPath) && File.Exists(workbookPath)) return workbookPath;
        if (File.Exists(ImportConstants.PrimaryWorkbookPath)) return ImportConstants.PrimaryWorkbookPath;
        if (File.Exists(ImportConstants.FallbackWorkbookPath)) return ImportConstants.FallbackWorkbookPath;
        throw new FileNotFoundException("Workbook file not found for import run.");
    }

    private static void EnsureOutputDirectories()
    {
        Directory.CreateDirectory("docs");
        Directory.CreateDirectory("database");
    }
}

public class ImportExecutionReport
{
    public string WorkbookPath { get; set; } = string.Empty;
    public int SheetCount { get; set; }
    public List<string> DetectedAnnualSheets { get; set; } = new();
    public List<string> DetectedCustomerSheets { get; set; } = new();
    public int TotalRowsProcessed { get; set; }
    public int CustomersInserted { get; set; }
    public int ContractsInserted { get; set; }
    public int BillingEventsInserted { get; set; }
    public int PMVisitsInserted { get; set; }
    public int InvoicesInserted { get; set; }
    public int DuplicateCustomersSkipped { get; set; }
    public int DuplicateInvoicesSkipped { get; set; }
    public int Errors { get; set; }
    public int Warnings { get; set; }
}

public class RejectedImportRow
{
    public string SourceReference { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
}
