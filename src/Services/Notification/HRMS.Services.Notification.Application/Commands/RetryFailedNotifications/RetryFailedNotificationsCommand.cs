using MediatR;

namespace HRMS.Services.Notification.Application.Commands.RetryFailedNotifications;

public record RetryFailedNotificationsCommand : IRequest<int>
{
    public int BatchSize { get; init; } = 50;
}
