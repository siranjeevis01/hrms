using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Audit.Domain.Entities;

public class AuditTrail : BaseEntity
{
    public Guid AuditLogId { get; private set; }
    public string FieldName { get; private set; } = string.Empty;
    public string? OldValue { get; private set; }
    public string? NewValue { get; private set; }
    public new string TenantId { get; private set; } = string.Empty;

    private AuditTrail() { }

    public static AuditTrail Create(
        Guid auditLogId,
        string fieldName,
        string? oldValue,
        string? newValue,
        string tenantId)
    {
        return new AuditTrail
        {
            Id = Guid.NewGuid(),
            AuditLogId = auditLogId,
            FieldName = fieldName,
            OldValue = oldValue,
            NewValue = newValue,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
