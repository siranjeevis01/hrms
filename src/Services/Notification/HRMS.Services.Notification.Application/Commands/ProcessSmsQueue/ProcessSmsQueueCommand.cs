using MediatR;

namespace HRMS.Services.Notification.Application.Commands.ProcessSmsQueue;

public record ProcessSmsQueueCommand : IRequest<int>
{
    public int BatchSize { get; init; } = 50;
}
