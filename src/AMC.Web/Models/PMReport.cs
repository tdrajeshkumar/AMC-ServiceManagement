using System.ComponentModel.DataAnnotations;

namespace AMC.Web.Models;

public class PMReport
{
    public int PMReportId { get; set; }
    public int PMVisitId { get; set; }
    [MaxLength(200)] public string? FileName { get; set; }
    public string? FilePath { get; set; }
    [MaxLength(100)] public string? ContentType { get; set; }
    public int? FileSizeKB { get; set; }
    public string? Summary { get; set; }
    public DateTime? UploadedDate { get; set; }

    public PMVisit PMVisit { get; set; } = null!;
}
