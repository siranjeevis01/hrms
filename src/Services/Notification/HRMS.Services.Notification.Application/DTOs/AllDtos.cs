using HRMS.Services.Notification.Domain.Enums;

namespace HRMS.Services.Notification.Application.DTOs;

public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
}

public class NotificationDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public NotificationType Type { get; set; }
    public NotificationCategory Category { get; set; }
    public NotificationPriority Priority { get; set; }
    public NotificationStatus Status { get; set; }
    public NotificationChannel Channel { get; set; }
    public DateTime? SentAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public DateTime? ReadAt { get; set; }
    public DateTime? FailedAt { get; set; }
    public string? FailureReason { get; set; }
    public string? Data { get; set; }
    public string? ActionUrl { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class NotificationListDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public NotificationType Type { get; set; }
    public NotificationCategory Category { get; set; }
    public NotificationPriority Priority { get; set; }
    public NotificationStatus Status { get; set; }
    public bool IsRead { get; set; }
    public string? ActionUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class NotificationTemplateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public NotificationCategory Category { get; set; }
    public NotificationChannel Channel { get; set; }
    public string? Subject { get; set; }
    public string Body { get; set; } = string.Empty;
    public string? Variables { get; set; }
    public bool IsActive { get; set; }
    public string Language { get; set; } = "en";
    public DateTime CreatedAt { get; set; }
}

public class NotificationPreferenceDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public NotificationCategory Category { get; set; }
    public NotificationChannel Channel { get; set; }
    public bool IsEnabled { get; set; }
    public NotificationFrequency Frequency { get; set; }
    public TimeOnly? QuietHoursStart { get; set; }
    public TimeOnly? QuietHoursEnd { get; set; }
}

public class NotificationGroupDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Members { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class DeliveryLogDto
{
    public Guid Id { get; set; }
    public Guid NotificationId { get; set; }
    public NotificationChannel Channel { get; set; }
    public string Provider { get; set; } = string.Empty;
    public string? ProviderMessageId { get; set; }
    public NotificationStatus Status { get; set; }
    public string? Response { get; set; }
    public int AttemptCount { get; set; }
    public DateTime? LastAttemptAt { get; set; }
    public DateTime? NextRetryAt { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class NotificationStatsDto
{
    public int TotalSent { get; set; }
    public int TotalDelivered { get; set; }
    public int TotalRead { get; set; }
    public int TotalFailed { get; set; }
    public int TotalPending { get; set; }
    public List<CategoryStatDto> ByCategory { get; set; } = new();
    public List<ChannelStatDto> ByChannel { get; set; } = new();
}

public class CategoryStatDto
{
    public NotificationCategory Category { get; set; }
    public int Count { get; set; }
}

public class ChannelStatDto
{
    public NotificationChannel Channel { get; set; }
    public int Count { get; set; }
}

public class SendNotificationRequestDto
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public NotificationType Type { get; set; }
    public NotificationCategory Category { get; set; }
    public NotificationPriority Priority { get; set; } = NotificationPriority.Normal;
    public NotificationChannel Channel { get; set; }
    public string? Data { get; set; }
    public string? ActionUrl { get; set; }
    public string? TemplateName { get; set; }
    public Dictionary<string, string>? TemplateVariables { get; set; }
}

public class EmailDto
{
    public string To { get; set; } = string.Empty;
    public string? Cc { get; set; }
    public string? Bcc { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public bool IsHtml { get; set; } = true;
    public NotificationPriority Priority { get; set; } = NotificationPriority.Normal;
    public string? Attachments { get; set; }
    public DateTime? ScheduledAt { get; set; }
}

public class SmsDto
{
    public string PhoneNumber { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Provider { get; set; } = "Twilio";
}
