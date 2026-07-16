using HRMS.Services.Audit.Domain.Enums;

namespace HRMS.Services.Audit.Application.DTOs;

public class AuditLogDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public AuditActionType ActionType { get; set; }
    public AuditEntityType EntityType { get; set; }
    public string EntityId { get; set; } = string.Empty;
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public string TenantId { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public List<AuditTrailDto> AuditTrails { get; set; } = new();
}
