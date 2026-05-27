using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMC.Web.Models;

public class Customer
{
    public int CustomerId { get; set; }

    [Required, MaxLength(200)]
    public string CustomerName { get; set; } = string.Empty;

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public string? NormalizedCustomerName { get; private set; }

    [MaxLength(100)] public string? ContactPerson { get; set; }
    [MaxLength(50)] public string? Phone { get; set; }
    [MaxLength(100)] public string? Email { get; set; }
    public string? Address { get; set; }
    [MaxLength(50)] public string? GSTNumber { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; } = true;
    [MaxLength(100)] public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    [MaxLength(100)] public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }

    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
