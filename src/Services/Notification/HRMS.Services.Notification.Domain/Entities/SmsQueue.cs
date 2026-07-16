using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Notification.Domain.Entities;

public class SmsQueue : AggregateRoot
{
    public string PhoneNumber { get; private set; } = string.Empty;
    public string Message { get; private set; } = string.Empty;
    public SmsQueueStatus Status { get; private set; }
    public string Provider { get; private set; } = "Twilio";
    public string? ProviderMessageId { get; private set; }
    public int RetryCount { get; private set; }
    public int MaxRetries { get; private set; }
    public DateTime? SentAt { get; private set; }
    public string? LastError { get; private set; }
    public Guid? TenantId { get; private set; }

    private SmsQueue() { }

    public static SmsQueue Create(
        string phoneNumber,
        string message,
        string provider = "Twilio",
        Guid? tenantId = null,
        int maxRetries = 3)
    {
        return new SmsQueue
        {
            Id = Guid.NewGuid(),
            PhoneNumber = phoneNumber,
            Message = message,
            Status = SmsQueueStatus.Queued,
            Provider = provider,
            RetryCount = 0,
            MaxRetries = maxRetries,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void MarkAsSent(string? providerMessageId = null)
    {
        Status = SmsQueueStatus.Sent;
        ProviderMessageId = providerMessageId;
        SentAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsFailed(string? error = null)
    {
        RetryCount++;
        LastError = error;
        if (RetryCount >= MaxRetries)
            Status = SmsQueueStatus.Failed;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsProcessing()
    {
        Status = SmsQueueStatus.Processing;
        UpdatedAt = DateTime.UtcNow;
    }

    public bool CanRetry => RetryCount < MaxRetries;
}

public enum SmsQueueStatus
{
    Queued = 0,
    Processing = 1,
    Sent = 2,
    Failed = 3
}
