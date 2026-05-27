using System.ComponentModel.DataAnnotations;

namespace AMC.Web.Models;

public class PMScheduleTemplate
{
    public int PMScheduleTemplateId { get; set; }
    public int ContractId { get; set; }
    [MaxLength(50)] public string? CycleCode { get; set; }
    public int? CycleIntervalMonths { get; set; }
    [MaxLength(30)] public string? AdvanceOrPostPM { get; set; }
    public DateOnly? AnchorDate { get; set; }
    [MaxLength(200)] public string? OriginalRuleText { get; set; }
    public bool IsActive { get; set; } = true;

    public Contract Contract { get; set; } = null!;
}
