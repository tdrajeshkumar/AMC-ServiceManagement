using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AMC.Web.Models;

public class Contract
{
    public int ContractId { get; set; }
    public int CustomerId { get; set; }
    [MaxLength(50)] public string? ContractType { get; set; }
    [MaxLength(300)] public string? ProductCovered { get; set; }
    [Precision(18,2)] public decimal? ContractAmount { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    [MaxLength(50)] public string? BillingCycle { get; set; }
    [MaxLength(50)] public string? PMCycle { get; set; }
    [MaxLength(200)] public string? PMCycleOriginalText { get; set; }
    [MaxLength(200)] public string? PMScheduleText { get; set; }
    [MaxLength(200)] public string? PMMonthText { get; set; }
    [MaxLength(100)] public string? PaymentTerms { get; set; }
    [MaxLength(200)] public string? PaymentTermsOriginalText { get; set; }
    [MaxLength(100)] public string? PONumber { get; set; }
    public string? Notes { get; set; }
    [MaxLength(50)] public string? Status { get; set; }
    public int? ParentContractId { get; set; }
    [MaxLength(20)] public string? RenewalYear { get; set; }
    public int? RenewalReminderDays { get; set; }
    [MaxLength(100)] public string? SourceSheet { get; set; }
    public int? SourceRowStart { get; set; }
    public int? SourceRowEnd { get; set; }
    [MaxLength(100)] public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    [MaxLength(100)] public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }

    public Customer Customer { get; set; } = null!;
    public Contract? ParentContract { get; set; }
    public ICollection<Contract> ChildContracts { get; set; } = new List<Contract>();
    public ICollection<BillingEvent> BillingEvents { get; set; } = new List<BillingEvent>();
    public ICollection<PMVisit> PMVisits { get; set; } = new List<PMVisit>();
    public ICollection<PMScheduleTemplate> PMScheduleTemplates { get; set; } = new List<PMScheduleTemplate>();
    public ICollection<ContractScopeDetail> ContractScopeDetails { get; set; } = new List<ContractScopeDetail>();
}
