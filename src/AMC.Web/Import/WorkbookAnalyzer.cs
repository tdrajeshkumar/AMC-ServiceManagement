using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace AMC.Web.Import;

public class WorkbookAnalyzer
{
    public WorkbookAnalysis Analyze(string workbookPath)
    {
        using var fs = File.OpenRead(workbookPath);
        IWorkbook workbook = new XSSFWorkbook(fs);

        var result = new WorkbookAnalysis { WorkbookPath = workbookPath };
        for (int i = 0; i < workbook.NumberOfSheets; i++)
        {
            var sheet = workbook.GetSheetAt(i);
            var headerRowIndex = DetectHeaderRow(sheet);
            var headers = ReadHeaders(sheet, headerRowIndex);
            var isAnnual = IsAnnualPlanningSheet(sheet.SheetName, headers);
            var isItem = !isAnnual;

            var dataRows = CountDataRows(sheet, headerRowIndex);
            result.Sheets.Add(new SheetAnalysis
            {
                SheetName = sheet.SheetName,
                HeaderRow = headerRowIndex + 1,
                Headers = headers,
                IsAnnualPlanningSheet = isAnnual,
                IsCustomerItemSheet = isItem,
                MergedRangeCount = sheet.NumMergedRegions,
                DataRowCount = dataRows
            });
        }

        return result;
    }

    private static int DetectHeaderRow(ISheet sheet)
    {
        var max = Math.Min(sheet.LastRowNum, 25);
        var bestIndex = 0;
        var bestScore = -1;
        for (int r = 0; r <= max; r++)
        {
            var row = sheet.GetRow(r);
            if (row == null) continue;
            var score = 0;
            for (int c = row.FirstCellNum; c < row.LastCellNum; c++)
            {
                var cell = row.GetCell(c);
                if (cell == null) continue;
                var txt = cell.ToString()?.Trim();
                if (!string.IsNullOrWhiteSpace(txt)) score++;
            }
            if (score > bestScore)
            {
                bestScore = score;
                bestIndex = r;
            }
        }
        return bestIndex;
    }

    private static List<string> ReadHeaders(ISheet sheet, int headerRow)
    {
        var row = sheet.GetRow(headerRow);
        if (row == null) return new List<string>();

        var headers = new List<string>();
        for (int c = row.FirstCellNum; c < row.LastCellNum; c++)
        {
            headers.Add(row.GetCell(c)?.ToString()?.Trim() ?? string.Empty);
        }
        return headers;
    }

    private static bool IsAnnualPlanningSheet(string sheetName, List<string> headers)
    {
        var byName = ImportConstants.AnnualSheetNameHints.Any(h => sheetName.Contains(h, StringComparison.OrdinalIgnoreCase));
        var hitCount = ImportConstants.AnnualHeaderHints.Count(h => headers.Any(x => string.Equals(x, h, StringComparison.OrdinalIgnoreCase)));
        return byName || hitCount >= 3;
    }

    private static int CountDataRows(ISheet sheet, int headerRow)
    {
        int count = 0;
        for (int r = headerRow + 1; r <= sheet.LastRowNum; r++)
        {
            var row = sheet.GetRow(r);
            if (row == null) continue;
            bool any = false;
            for (int c = row.FirstCellNum; c < row.LastCellNum; c++)
            {
                if (!string.IsNullOrWhiteSpace(row.GetCell(c)?.ToString()))
                {
                    any = true; break;
                }
            }
            if (any) count++;
        }
        return count;
    }
}
