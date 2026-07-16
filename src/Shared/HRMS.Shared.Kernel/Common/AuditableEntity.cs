using HRMS.Shared.Kernel.Enums;

namespace HRMS.Shared.Kernel.Common;

public abstract class AuditableEntity : BaseEntity
{
    public List<AuditEntry> AuditTrail { get; set; } = new();

    public void RecordAudit(AuditAction action, string? performedBy, string? details = null)
    {
        var entry = new AuditEntry
        {
            Id = Guid.NewGuid(),
            Action = action,
            PerformedBy = performedBy,
            PerformedAt = DateTime.UtcNow,
            Details = details,
            EntityId = Id,
            EntityType = GetType().Name
        };

        AuditTrail.Add(entry);
        UpdateTimestamp(performedBy);
    }
}

public class AuditEntry
{
    public Guid Id { get; set; }
    public AuditAction Action { get; set; }
    public string? PerformedBy { get; set; }
    public DateTime PerformedAt { get; set; }
    public string? Details { get; set; }
    public string? PreviousValues { get; set; }
    public string? NewValues { get; set; }
    public Guid EntityId { get; set; }
    public string EntityType { get; set; } = string.Empty;
}
