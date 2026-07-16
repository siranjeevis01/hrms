using HRMS.Services.Audit.Domain.Enums;

namespace HRMS.Services.Audit.Application.DTOs;

public class DataChangeLogDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public AuditEntityType EntityType { get; set; }
    public string EntityId { get; set; } = string.Empty;
    public string ChangeType { get; set; } = string.Empty;
    public string? SerializedData { get; set; }
    public DateTime Timestamp { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
