using System.ComponentModel.DataAnnotations;

namespace AMC.Web.Models;

public class AuditLog
{
    public long AuditLogId { get; set; }
    [MaxLength(100)] public string? TableName { get; set; }
    [MaxLength(100)] public string? ModuleName { get; set; }
    [MaxLength(100)] public string? RecordId { get; set; }
    [MaxLength(100)] public string? FieldName { get; set; }
    [MaxLength(50)] public string? ActionType { get; set; }
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    [MaxLength(100)] public string? ChangedBy { get; set; }
    public DateTime? ChangedDate { get; set; }
    [MaxLength(100)] public string? IPAddress { get; set; }
}
