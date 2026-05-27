using System.ComponentModel.DataAnnotations;

namespace AMC.Web.Models;

public class InvoiceRemarkHistory
{
    public int InvoiceRemarkHistoryId { get; set; }
    public int InvoiceId { get; set; }
    public string RemarkText { get; set; } = string.Empty;
    [MaxLength(100)] public string? RemarkContext { get; set; }
    public DateOnly? RemarkDate { get; set; }
    [MaxLength(100)] public string? SourceSheet { get; set; }
    public int? SourceRow { get; set; }
    [MaxLength(20)] public string? SourceColumn { get; set; }
    public DateTime CreatedOn { get; set; }

    public Invoice Invoice { get; set; } = null!;
}
