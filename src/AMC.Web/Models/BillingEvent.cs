using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AMC.Web.Models;

public class BillingEvent
{
    public int BillingEventId { get; set; }
    public int ContractId { get; set; }
    public DateOnly? BillingPeriodFrom { get; set; }
    public DateOnly? BillingPeriodTo { get; set; }
    public DateOnly? PlannedInvoiceDate { get; set; }
    [Precision(18,2)] public decimal? PlannedAmount { get; set; }
    [MaxLength(50)] public string? Status { get; set; }
    [MaxLength(100)] public string? GeneratedByRule { get; set; }
    [MaxLength(100)] public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    [MaxLength(100)] public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }

    public Contract Contract { get; set; } = null!;
    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
