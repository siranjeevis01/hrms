using MediatR;

namespace HRMS.Services.Notification.Application.Commands.DeleteNotification;

public record DeleteNotificationCommand : IRequest<Unit>
{
    public Guid NotificationId { get; init; }
    public Guid UserId { get; init; }
}
