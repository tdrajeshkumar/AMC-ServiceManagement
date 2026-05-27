using System.ComponentModel.DataAnnotations;

namespace AMC.Web.Models;

public class FollowUp
{
    public int FollowUpId { get; set; }
    public int InvoiceId { get; set; }
    public DateOnly? FollowUpDate { get; set; }
    [MaxLength(100)] public string? ContactPerson { get; set; }
    public string? Discussion { get; set; }
    public DateOnly? NextFollowUpDate { get; set; }
    [MaxLength(50)] public string? Status { get; set; }
    [MaxLength(100)] public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    [MaxLength(100)] public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }

    public Invoice Invoice { get; set; } = null!;
}
