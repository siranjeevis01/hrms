using HRMS.Services.Chat.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Chat.Domain.Entities;

public class UserPresence : BaseEntity
{
    public Guid EmployeeId { get; private set; }
    public PresenceStatus PresenceStatus { get; private set; }
    public DateTime? LastSeenAt { get; private set; }

    private UserPresence() { }

    public static UserPresence Create(
        Guid employeeId,
        PresenceStatus status,
        Guid tenantId)
    {
        return new UserPresence
        {
            Id = Guid.NewGuid(),
            EmployeeId = employeeId,
            PresenceStatus = status,
            LastSeenAt = DateTime.UtcNow,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void UpdateStatus(PresenceStatus status)
    {
        PresenceStatus = status;
        LastSeenAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
