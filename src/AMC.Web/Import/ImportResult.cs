namespace AMC.Web.Import;

public class ImportResult
{
    public int TotalRows { get; set; }
    public int Inserted { get; set; }
    public int Updated { get; set; }
    public int Skipped { get; set; }
    public int Errors { get; set; }
    public int Warnings { get; set; }

    public List<string> Messages { get; set; } = new();
}

public class WorkbookAnalysis
{
    public string WorkbookPath { get; set; } = string.Empty;
    public List<SheetAnalysis> Sheets { get; set; } = new();
}

public class SheetAnalysis
{
    public string SheetName { get; set; } = string.Empty;
    public bool IsAnnualPlanningSheet { get; set; }
    public bool IsCustomerItemSheet { get; set; }
    public int HeaderRow { get; set; }
    public List<string> Headers { get; set; } = new();
    public int MergedRangeCount { get; set; }
    public int DataRowCount { get; set; }
}

public class StagingContractRow
{
    public string SourceSheet { get; set; } = string.Empty;
    public int SourceRow { get; set; }
    public string? CustomerName { get; set; }
    public string? ProductCovered { get; set; }
    public decimal? TotalAmount { get; set; }
    public string? PeriodText { get; set; }
    public string? PmCycle { get; set; }
    public string? PmSchedule { get; set; }
    public string? RemarksRaw { get; set; }
}

public class StagingBillingRow
{
    public string SourceSheet { get; set; } = string.Empty;
    public int SourceRow { get; set; }
    public string? CustomerName { get; set; }
    public decimal? PlannedAmount { get; set; }
    public string? RemarksRaw { get; set; }
}

public class StagingPmRow
{
    public string SourceSheet { get; set; } = string.Empty;
    public int SourceRow { get; set; }
    public string? CustomerName { get; set; }
    public string? PmCycle { get; set; }
    public string? PmMonth { get; set; }
    public string? RemarksRaw { get; set; }
}
