using HRMS.Services.Notification.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Notification.Domain.Entities;

public class NotificationDeliveryLog : AggregateRoot
{
    public Guid NotificationId { get; private set; }
    public NotificationChannel Channel { get; private set; }
    public string Provider { get; private set; } = string.Empty;
    public string? ProviderMessageId { get; private set; }
    public new NotificationStatus Status { get; private set; }
    public string? Response { get; private set; }
    public int AttemptCount { get; private set; }
    public DateTime? LastAttemptAt { get; private set; }
    public DateTime? NextRetryAt { get; private set; }
    public new Guid? TenantId { get; private set; }

    private NotificationDeliveryLog() { }

    public static NotificationDeliveryLog Create(
        Guid notificationId,
        NotificationChannel channel,
        string provider,
        Guid? tenantId = null)
    {
        return new NotificationDeliveryLog
        {
            Id = Guid.NewGuid(),
            NotificationId = notificationId,
            Channel = channel,
            Provider = provider,
            Status = NotificationStatus.Pending,
            AttemptCount = 0,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void RecordAttempt(string providerMessageId, NotificationStatus status, string? response = null)
    {
        AttemptCount++;
        ProviderMessageId = providerMessageId;
        Status = status;
        Response = response;
        LastAttemptAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ScheduleRetry(TimeSpan delay)
    {
        NextRetryAt = DateTime.UtcNow.Add(delay);
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsCompleted()
    {
        Status = NotificationStatus.Sent;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsFailed(string? response = null)
    {
        Status = NotificationStatus.Failed;
        Response = response;
        UpdatedAt = DateTime.UtcNow;
    }
}
