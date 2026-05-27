using System.ComponentModel.DataAnnotations;

namespace AMC.Web.Models;

public class PMVisit
{
    public int PMVisitId { get; set; }
    public int ContractId { get; set; }
    public int? EngineerId { get; set; }
    public DateOnly? ScheduledDate { get; set; }
    public DateOnly? CompletedDate { get; set; }
    [MaxLength(50)] public string? Status { get; set; }
    public string? VisitRemarks { get; set; }
    [MaxLength(50)] public string? ScheduleSource { get; set; }
    public int? SequenceNo { get; set; }
    [MaxLength(100)] public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    [MaxLength(100)] public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }

    public Contract Contract { get; set; } = null!;
    public Engineer? Engineer { get; set; }
    public ICollection<PMReport> PMReports { get; set; } = new List<PMReport>();
}
