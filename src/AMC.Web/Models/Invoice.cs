using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMC.Web.Models;

public class Invoice
{
    public int InvoiceId { get; set; }
    public int? BillingEventId { get; set; }
    [MaxLength(100)] public string? InvoiceNumber { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public string? NormalizedInvoiceNumber { get; private set; }
    public DateOnly? InvoiceDate { get; set; }
    public DateOnly? BillingPeriodFrom { get; set; }
    public DateOnly? BillingPeriodTo { get; set; }
    [Precision(18,2)] public decimal? InvoiceAmount { get; set; }
    [Precision(18,2)] public decimal? ReceivedAmount { get; set; }
    public DateOnly? DueDate { get; set; }
    [MaxLength(50)] public string? Status { get; set; }
    public string? Remarks { get; set; }
    [MaxLength(100)] public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    [MaxLength(100)] public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }

    public BillingEvent? BillingEvent { get; set; }
    public ICollection<FollowUp> FollowUps { get; set; } = new List<FollowUp>();
    public ICollection<InvoiceRemarkHistory> InvoiceRemarkHistories { get; set; } = new List<InvoiceRemarkHistory>();
}
