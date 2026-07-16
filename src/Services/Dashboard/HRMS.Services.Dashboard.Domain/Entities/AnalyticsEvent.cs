using HRMS.Services.Dashboard.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Dashboard.Domain.Entities;

public class AnalyticsEvent : BaseEntity
{
    public AnalyticsEventType EventType { get; private set; }
    public string? EntityId { get; private set; }
    public string? EntityType { get; private set; }
    public Guid? EmployeeId { get; private set; }
    public string? Data { get; private set; }
    public DateTime Timestamp { get; private set; }
    public string TenantId { get; private set; } = string.Empty;

    private AnalyticsEvent() { }

    public static AnalyticsEvent Create(
        AnalyticsEventType eventType,
        string? entityId,
        string? entityType,
        Guid? employeeId,
        string? data,
        string tenantId)
    {
        return new AnalyticsEvent
        {
            Id = Guid.NewGuid(),
            EventType = eventType,
            EntityId = entityId,
            EntityType = entityType,
            EmployeeId = employeeId,
            Data = data,
            Timestamp = DateTime.UtcNow,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
