using HRMS.Services.Notification.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Notification.Domain.Entities;

public class NotificationPreference : AggregateRoot
{
    public Guid UserId { get; private set; }
    public NotificationCategory Category { get; private set; }
    public NotificationChannel Channel { get; private set; }
    public bool IsEnabled { get; private set; }
    public NotificationFrequency Frequency { get; private set; }
    public TimeOnly? QuietHoursStart { get; private set; }
    public TimeOnly? QuietHoursEnd { get; private set; }
    public Guid? TenantId { get; private set; }

    private NotificationPreference() { }

    public static NotificationPreference Create(
        Guid userId,
        NotificationCategory category,
        NotificationChannel channel,
        bool isEnabled = true,
        NotificationFrequency frequency = NotificationFrequency.Immediate,
        TimeOnly? quietHoursStart = null,
        TimeOnly? quietHoursEnd = null,
        Guid? tenantId = null)
    {
        return new NotificationPreference
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Category = category,
            Channel = channel,
            IsEnabled = isEnabled,
            Frequency = frequency,
            QuietHoursStart = quietHoursStart,
            QuietHoursEnd = quietHoursEnd,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(bool isEnabled, NotificationFrequency frequency,
        TimeOnly? quietHoursStart = null, TimeOnly? quietHoursEnd = null)
    {
        IsEnabled = isEnabled;
        Frequency = frequency;
        QuietHoursStart = quietHoursStart;
        QuietHoursEnd = quietHoursEnd;
        UpdatedAt = DateTime.UtcNow;
    }

    public bool IsInQuietHours()
    {
        if (!QuietHoursStart.HasValue || !QuietHoursEnd.HasValue)
            return false;

        var now = TimeOnly.FromDateTime(DateTime.UtcNow);
        var start = QuietHoursStart.Value;
        var end = QuietHoursEnd.Value;

        if (start <= end)
            return now >= start && now <= end;
        else
            return now >= start || now <= end;
    }
}
