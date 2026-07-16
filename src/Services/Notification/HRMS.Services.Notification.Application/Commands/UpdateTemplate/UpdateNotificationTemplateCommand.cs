using HRMS.Services.Notification.Domain.Enums;
using MediatR;

namespace HRMS.Services.Notification.Application.Commands.UpdateTemplate;

public record UpdateNotificationTemplateCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public NotificationCategory Category { get; init; }
    public NotificationChannel Channel { get; init; }
    public string? Subject { get; init; }
    public string Body { get; init; } = string.Empty;
    public string? Variables { get; init; }
    public string Language { get; init; } = "en";
}
