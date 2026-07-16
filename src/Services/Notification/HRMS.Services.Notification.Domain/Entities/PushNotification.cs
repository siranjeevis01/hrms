using HRMS.Services.Notification.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Notification.Domain.Entities;

public class PushNotification : AggregateRoot
{
    public Guid UserId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Body { get; private set; } = string.Empty;
    public string? Data { get; private set; }
    public string? DeviceTokens { get; private set; }
    public NotificationStatus Status { get; private set; }
    public DateTime? SentAt { get; private set; }
    public string? Response { get; private set; }
    public Guid? TenantId { get; private set; }

    private PushNotification() { }

    public static PushNotification Create(
        Guid userId,
        string title,
        string body,
        string? deviceTokens = null,
        string? data = null,
        Guid? tenantId = null)
    {
        return new PushNotification
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Title = title,
            Body = body,
            DeviceTokens = deviceTokens,
            Data = data,
            Status = NotificationStatus.Pending,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void MarkAsSent(string? response = null)
    {
        Status = NotificationStatus.Sent;
        SentAt = DateTime.UtcNow;
        Response = response;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsFailed(string? reason = null)
    {
        Status = NotificationStatus.Failed;
        Response = reason;
        UpdatedAt = DateTime.UtcNow;
    }

    public List<string> GetDeviceTokens()
    {
        if (string.IsNullOrEmpty(DeviceTokens))
            return new List<string>();

        return System.Text.Json.JsonSerializer.Deserialize<List<string>>(DeviceTokens) ?? new List<string>();
    }
}
