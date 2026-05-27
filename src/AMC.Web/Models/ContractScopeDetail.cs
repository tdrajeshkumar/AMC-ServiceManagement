using System.ComponentModel.DataAnnotations;

namespace AMC.Web.Models;

public class ContractScopeDetail
{
    public int ContractScopeDetailId { get; set; }
    public int ContractId { get; set; }
    [MaxLength(500)] public string? ItemDescription { get; set; }
    public int? Quantity { get; set; }
    [MaxLength(100)] public string? WarrantyInfo { get; set; }
    [MaxLength(50)] public string? InstallationYear { get; set; }
    [MaxLength(100)] public string? SerialNumber { get; set; }
    [MaxLength(100)] public string? Category { get; set; }
    [MaxLength(100)] public string? SourceSection { get; set; }
    public string? Notes { get; set; }

    public Contract Contract { get; set; } = null!;
}
