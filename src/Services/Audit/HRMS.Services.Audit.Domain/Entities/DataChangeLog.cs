using HRMS.Services.Audit.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Audit.Domain.Entities;

public class DataChangeLog : BaseEntity
{
    public Guid UserId { get; private set; }
    public AuditEntityType EntityType { get; private set; }
    public string EntityId { get; private set; } = string.Empty;
    public string ChangeType { get; private set; } = string.Empty;
    public string? SerializedData { get; private set; }
    public DateTime Timestamp { get; private set; }
    public new string TenantId { get; private set; } = string.Empty;

    private DataChangeLog() { }

    public static DataChangeLog Create(
        Guid userId,
        AuditEntityType entityType,
        string entityId,
        string changeType,
        string? serializedData,
        string tenantId)
    {
        return new DataChangeLog
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            EntityType = entityType,
            EntityId = entityId,
            ChangeType = changeType,
            SerializedData = serializedData,
            Timestamp = DateTime.UtcNow,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
