using MediatR;

namespace HRMS.Shared.Kernel.Interfaces;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IReadOnlyList<INotification> domainEvents, CancellationToken cancellationToken = default);
}
