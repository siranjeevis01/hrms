using HRMS.Services.Notification.Domain.Enums;
using MediatR;

namespace HRMS.Services.Notification.Application.Commands.CreateTemplate;

public record CreateNotificationTemplateCommand : IRequest<Guid>
{
    public string Name { get; init; } = string.Empty;
    public NotificationCategory Category { get; init; }
    public NotificationChannel Channel { get; init; }
    public string? Subject { get; init; }
    public string Body { get; init; } = string.Empty;
    public string? Variables { get; init; }
    public string Language { get; init; } = "en";
    public Guid? TenantId { get; init; }
}
