namespace AMC.Web.Import;

public static class ImportConstants
{
    public const string PrimaryWorkbookPath = "data-source/AMC-Renewal-2025-2026.xlsx";
    public const string FallbackWorkbookPath = "data-source/AMCRenewal-2025-2026_1852026.xlsx";

    public static readonly string[] AnnualSheetNameHints = { "AMC 2026", "AMC 2025", "AMC 2024" };
    public static readonly string[] AnnualHeaderHints = { "CLIENT", "TOTAL AMOUNT", "PM Cycle", "PM Schedule", "Billed" };

    public static readonly string[] ValidBillingCycles = { "Monthly", "Quarterly", "Half-Yearly", "Yearly" };
    public static readonly string[] ValidPmCycles = { "Monthly", "Quarterly", "Half-Yearly", "Yearly" };
}
