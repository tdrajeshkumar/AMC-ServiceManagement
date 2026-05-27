using System.ComponentModel.DataAnnotations;

namespace AMC.Web.Models;

public class Engineer
{
    public int EngineerId { get; set; }
    [MaxLength(100)] public string? Name { get; set; }
    [MaxLength(50)] public string? Phone { get; set; }
    [MaxLength(100)] public string? Email { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<PMVisit> PMVisits { get; set; } = new List<PMVisit>();
}
