using HRMS.Services.Notification.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Notification.Domain.Entities;

public class EmailQueue : AggregateRoot
{
    public string To { get; private set; } = string.Empty;
    public string? Cc { get; private set; }
    public string? Bcc { get; private set; }
    public string Subject { get; private set; } = string.Empty;
    public string Body { get; private set; } = string.Empty;
    public bool IsHtml { get; private set; }
    public string? Attachments { get; private set; }
    public new EmailQueueStatus Status { get; private set; }
    public NotificationPriority Priority { get; private set; }
    public DateTime? ScheduledAt { get; private set; }
    public DateTime? SentAt { get; private set; }
    public int RetryCount { get; private set; }
    public int MaxRetries { get; private set; }
    public string? LastError { get; private set; }
    public new Guid? TenantId { get; private set; }

    private EmailQueue() { }

    public static EmailQueue Create(
        string to,
        string subject,
        string body,
        NotificationPriority priority = NotificationPriority.Normal,
        string? cc = null,
        string? bcc = null,
        bool isHtml = true,
        string? attachments = null,
        DateTime? scheduledAt = null,
        Guid? tenantId = null,
        int maxRetries = 3)
    {
        return new EmailQueue
        {
            Id = Guid.NewGuid(),
            To = to,
            Cc = cc,
            Bcc = bcc,
            Subject = subject,
            Body = body,
            IsHtml = isHtml,
            Attachments = attachments,
            Status = EmailQueueStatus.Queued,
            Priority = priority,
            ScheduledAt = scheduledAt,
            RetryCount = 0,
            MaxRetries = maxRetries,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void MarkAsProcessing()
    {
        Status = EmailQueueStatus.Processing;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsSent()
    {
        Status = EmailQueueStatus.Sent;
        SentAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsFailed(string? error = null)
    {
        RetryCount++;
        LastError = error;
        if (RetryCount >= MaxRetries)
            Status = EmailQueueStatus.Failed;
        UpdatedAt = DateTime.UtcNow;
    }

    public bool CanRetry => RetryCount < MaxRetries;
}

public enum EmailQueueStatus
{
    Queued = 0,
    Processing = 1,
    Sent = 2,
    Failed = 3
}
