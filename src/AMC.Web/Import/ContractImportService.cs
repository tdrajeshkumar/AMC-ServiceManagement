using AMC.Web.Data;
using AMC.Web.Models;

namespace AMC.Web.Import;

public class ContractImportService
{
    private readonly ApplicationDbContext _db;

    public ContractImportService(ApplicationDbContext db) => _db = db;

    public async Task<(int inserted, int updated)> ImportContractsAsync(
        IEnumerable<StagingContractRow> contracts,
        IDictionary<string, Customer> customerMap,
        CancellationToken cancellationToken = default)
    {
        int inserted = 0, updated = 0;

        foreach (var row in contracts.Where(x => !string.IsNullOrWhiteSpace(x.CustomerName)))
        {
            var customer = customerMap[row.CustomerName!.Trim()];

            var contract = new Contract
            {
                CustomerId = customer.CustomerId,
                ProductCovered = row.ProductCovered,
                ContractAmount = row.TotalAmount,
                PMCycle = row.PmCycle,
                PMScheduleText = row.PmSchedule,
                Notes = row.RemarksRaw,
                SourceSheet = row.SourceSheet,
                SourceRowStart = row.SourceRow,
                CreatedBy = "import",
                CreatedDate = DateTime.UtcNow,
                Status = "Active"
            };

            _db.Contracts.Add(contract);
            inserted++;
        }

        await _db.SaveChangesAsync(cancellationToken);
        return (inserted, updated);
    }
}
