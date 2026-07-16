using HRMS.Services.Notification.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Notification.Domain.Entities;

public class Notification : AggregateRoot
{
    public Guid UserId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Message { get; private set; } = string.Empty;
    public NotificationType Type { get; private set; }
    public NotificationCategory Category { get; private set; }
    public NotificationPriority Priority { get; private set; }
    public NotificationStatus Status { get; private set; }
    public NotificationChannel Channel { get; private set; }
    public DateTime? SentAt { get; private set; }
    public DateTime? DeliveredAt { get; private set; }
    public DateTime? ReadAt { get; private set; }
    public DateTime? FailedAt { get; private set; }
    public string? FailureReason { get; private set; }
    public string? Data { get; private set; }
    public string? ActionUrl { get; private set; }
    public bool IsRead { get; private set; }
    public Guid? TenantId { get; private set; }

    private Notification() { }

    public static Notification Create(
        Guid userId,
        string title,
        string message,
        NotificationType type,
        NotificationCategory category,
        NotificationPriority priority,
        NotificationChannel channel,
        string? data = null,
        string? actionUrl = null,
        Guid? tenantId = null)
    {
        var notification = new Notification
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Title = title,
            Message = message,
            Type = type,
            Category = category,
            Priority = priority,
            Channel = channel,
            Status = NotificationStatus.Pending,
            IsRead = false,
            Data = data,
            ActionUrl = actionUrl,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow
        };
        return notification;
    }

    public void MarkAsSent()
    {
        Status = NotificationStatus.Sent;
        SentAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsDelivered()
    {
        Status = NotificationStatus.Delivered;
        DeliveredAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsRead()
    {
        Status = NotificationStatus.Read;
        ReadAt = DateTime.UtcNow;
        IsRead = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsFailed(string reason)
    {
        Status = NotificationStatus.Failed;
        FailedAt = DateTime.UtcNow;
        FailureReason = reason;
        UpdatedAt = DateTime.UtcNow;
    }
}
