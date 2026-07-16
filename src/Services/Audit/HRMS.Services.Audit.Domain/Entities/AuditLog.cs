using HRMS.Services.Audit.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Audit.Domain.Entities;

public class AuditLog : AggregateRoot
{
    public Guid UserId { get; private set; }
    public string UserName { get; private set; } = string.Empty;
    public AuditActionType ActionType { get; private set; }
    public AuditEntityType EntityType { get; private set; }
    public string EntityId { get; private set; } = string.Empty;
    public string? OldValues { get; private set; }
    public string? NewValues { get; private set; }
    public string? IpAddress { get; private set; }
    public string? UserAgent { get; private set; }
    public string TenantId { get; private set; } = string.Empty;
    public DateTime Timestamp { get; private set; }

    private readonly List<AuditTrail> _auditTrails = new();
    public IReadOnlyCollection<AuditTrail> AuditTrails => _auditTrails.AsReadOnly();

    private AuditLog() { }

    public static AuditLog Create(
        Guid userId,
        string userName,
        AuditActionType actionType,
        AuditEntityType entityType,
        string entityId,
        string? oldValues,
        string? newValues,
        string? ipAddress,
        string? userAgent,
        string tenantId)
    {
        return new AuditLog
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            UserName = userName,
            ActionType = actionType,
            EntityType = entityType,
            EntityId = entityId,
            OldValues = oldValues,
            NewValues = newValues,
            IpAddress = ipAddress,
            UserAgent = userAgent,
            TenantId = tenantId,
            Timestamp = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void AddTrail(AuditTrail trail)
    {
        _auditTrails.Add(trail);
    }
}
