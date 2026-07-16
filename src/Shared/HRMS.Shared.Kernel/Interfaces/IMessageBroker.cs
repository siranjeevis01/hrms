namespace HRMS.Shared.Kernel.Interfaces;

public interface IMessageBroker
{
    Task PublishAsync<T>(T message, string? topic = null, CancellationToken cancellationToken = default) where T : class;
    Task SendAsync<T>(T message, string queue, CancellationToken cancellationToken = default) where T : class;
    Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request, string queue, CancellationToken cancellationToken = default)
        where TRequest : class
        where TResponse : class;
}
