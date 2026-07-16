using MediatR;

namespace HRMS.Services.Notification.Application.Commands.MarkAllAsRead;

public record MarkAllAsReadCommand : IRequest<int>
{
    public Guid UserId { get; init; }
}
