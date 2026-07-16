using MediatR;

namespace HRMS.Services.Notification.Application.Commands.MarkAsRead;

public record MarkAsReadCommand : IRequest<Unit>
{
    public Guid NotificationId { get; init; }
    public Guid UserId { get; init; }
}
