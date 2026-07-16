namespace HRMS.Services.Audit.Application.DTOs;

public class AuditTrailDto
{
    public Guid Id { get; set; }
    public Guid AuditLogId { get; set; }
    public string FieldName { get; set; } = string.Empty;
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
