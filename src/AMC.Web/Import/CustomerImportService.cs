using AMC.Web.Data;
using AMC.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace AMC.Web.Import;

public class CustomerImportService
{
    private readonly ApplicationDbContext _db;

    public CustomerImportService(ApplicationDbContext db) => _db = db;

    public async Task<(int inserted, int updated, Dictionary<string, Customer> map)> UpsertCustomersAsync(
        IEnumerable<StagingContractRow> rows,
        CancellationToken cancellationToken = default)
    {
        int inserted = 0, updated = 0;
        var map = new Dictionary<string, Customer>(StringComparer.OrdinalIgnoreCase);

        foreach (var group in rows.Where(x => !string.IsNullOrWhiteSpace(x.CustomerName)).GroupBy(x => x.CustomerName!.Trim()))
        {
            var key = group.Key;
            var normalized = key.ToUpperInvariant();
            var existing = await _db.Customers.FirstOrDefaultAsync(x => x.NormalizedCustomerName == normalized, cancellationToken);
            if (existing == null)
            {
                existing = new Customer { CustomerName = key, IsActive = true, CreatedDate = DateTime.UtcNow, CreatedBy = "import" };
                _db.Customers.Add(existing);
                inserted++;
            }
            else
            {
                existing.ModifiedDate = DateTime.UtcNow;
                existing.ModifiedBy = "import";
                updated++;
            }

            map[key] = existing;
        }

        await _db.SaveChangesAsync(cancellationToken);
        return (inserted, updated, map);
    }
}
