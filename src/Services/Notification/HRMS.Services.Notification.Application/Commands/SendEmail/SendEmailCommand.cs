using HRMS.Services.Notification.Domain.Enums;
using MediatR;

namespace HRMS.Services.Notification.Application.Commands.SendEmail;

public record SendEmailCommand : IRequest<Guid>
{
    public string To { get; init; } = string.Empty;
    public string? Cc { get; init; }
    public string? Bcc { get; init; }
    public string Subject { get; init; } = string.Empty;
    public string Body { get; init; } = string.Empty;
    public bool IsHtml { get; init; } = true;
    public NotificationPriority Priority { get; init; } = NotificationPriority.Normal;
    public string? Attachments { get; init; }
    public DateTime? ScheduledAt { get; init; }
    public Guid? TenantId { get; init; }
}
