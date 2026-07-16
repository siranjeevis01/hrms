using HRMS.Services.Notification.Domain.Enums;
using MediatR;

namespace HRMS.Services.Notification.Application.Commands.SendNotification;

public record SendNotificationCommand : IRequest<Guid>
{
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
    public NotificationType Type { get; init; }
    public NotificationCategory Category { get; init; }
    public NotificationPriority Priority { get; init; } = NotificationPriority.Normal;
    public NotificationChannel Channel { get; init; }
    public string? Data { get; init; }
    public string? ActionUrl { get; init; }
    public string? TemplateName { get; init; }
    public Dictionary<string, string>? TemplateVariables { get; init; }
}
