using AMC.Web.Data;
using AMC.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace AMC.Web.Import;

public class ImportValidationService
{
    private readonly ApplicationDbContext _db;

    public ImportValidationService(ApplicationDbContext db) => _db = db;

    public async Task<List<string>> ValidateAsync(
        IEnumerable<StagingContractRow> contracts,
        IEnumerable<StagingBillingRow> billings,
        IEnumerable<StagingPmRow> pms,
        CancellationToken cancellationToken = default)
    {
        var issues = new List<string>();

        var customerNames = contracts.Select(x => Normalize(x.CustomerName)).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
        var duplicateInputCustomers = customerNames.GroupBy(x => x).Where(g => g.Count() > 1).Select(g => g.Key!);
        foreach (var name in duplicateInputCustomers)
            issues.Add($"Duplicate customer in workbook import batch: {name}");

        var existingCustomers = await _db.Customers.Select(x => x.NormalizedCustomerName).ToListAsync(cancellationToken);
        foreach (var name in customerNames.Distinct())
            if (existingCustomers.Contains(name)) issues.Add($"Customer already exists (will update/upsert): {name}");

        foreach (var c in contracts)
        {
            if (!string.IsNullOrWhiteSpace(c.PeriodText) && c.PeriodText!.Split('-', 't').Length < 2)
                issues.Add($"Contract period may be invalid at {c.SourceSheet}:{c.SourceRow}");

            if (!string.IsNullOrWhiteSpace(c.PmCycle) && !ImportConstants.ValidPmCycles.Contains(c.PmCycle, StringComparer.OrdinalIgnoreCase))
                issues.Add($"Invalid PM cycle at {c.SourceSheet}:{c.SourceRow} -> {c.PmCycle}");
        }

        foreach (var b in billings)
            if (b.PlannedAmount.HasValue && b.PlannedAmount < 0)
                issues.Add($"Negative billing amount at {b.SourceSheet}:{b.SourceRow}");

        var invoiceDuplicates = await _db.Invoices
            .Where(i => i.InvoiceNumber != null)
            .GroupBy(i => i.NormalizedInvoiceNumber)
            .Where(g => g.Key != null && g.Count() > 1)
            .Select(g => g.Key!)
            .ToListAsync(cancellationToken);

        foreach (var inv in invoiceDuplicates)
            issues.Add($"Duplicate invoice number exists in database: {inv}");

        if (!pms.Any()) issues.Add("No PM rows detected from annual planning sheets.");

        return issues;
    }

    private static string? Normalize(string? s) => string.IsNullOrWhiteSpace(s) ? null : s.Trim().ToUpperInvariant();
}
