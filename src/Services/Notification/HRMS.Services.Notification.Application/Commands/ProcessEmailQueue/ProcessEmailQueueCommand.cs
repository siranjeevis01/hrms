using MediatR;

namespace HRMS.Services.Notification.Application.Commands.ProcessEmailQueue;

public record ProcessEmailQueueCommand : IRequest<int>
{
    public int BatchSize { get; init; } = 50;
}
