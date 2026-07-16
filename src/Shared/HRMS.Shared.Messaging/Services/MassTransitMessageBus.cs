using HRMS.Shared.Kernel.Interfaces;
using HRMS.Shared.Messaging.Interfaces;
using MassTransit;

namespace HRMS.Shared.Messaging.Services;

public class MassTransitMessageBus : IMessageBus, IMessageBroker
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly IBus _bus;

    public MassTransitMessageBus(
        IPublishEndpoint publishEndpoint,
        ISendEndpointProvider sendEndpointProvider,
        IBus bus)
    {
        _publishEndpoint = publishEndpoint;
        _sendEndpointProvider = sendEndpointProvider;
        _bus = bus;
    }

    public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class
    {
        await _publishEndpoint.Publish(message, cancellationToken);
    }

    public async Task PublishAsync<T>(T message, string? topic = null, CancellationToken cancellationToken = default) where T : class
    {
        await _publishEndpoint.Publish(message, cancellationToken);
    }

    public async Task SendAsync<T>(T message, CancellationToken cancellationToken = default) where T : class
    {
        var endpoint = await _sendEndpointProvider.GetSendEndpoint(
            new Uri("queue:hrms-default"));
        await endpoint.Send(message, cancellationToken);
    }

    public async Task SendAsync<T>(T message, string queue, CancellationToken cancellationToken = default) where T : class
    {
        var endpoint = await _sendEndpointProvider.GetSendEndpoint(
            new Uri($"queue:{queue}"));
        await endpoint.Send(message, cancellationToken);
    }

    public async Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request, string queue, CancellationToken cancellationToken = default)
        where TRequest : class
        where TResponse : class
    {
        var requestClient = _bus.CreateRequestClient<TRequest>(
            new Uri($"queue:{queue}"),
            TimeSpan.FromSeconds(30));

        var response = await requestClient.GetResponse<TResponse>(request, cancellationToken);
        return response.Message;
    }
}
