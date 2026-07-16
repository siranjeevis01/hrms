using HRMS.Shared.Kernel.Common;
using HRMS.Shared.Kernel.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace HRMS.Shared.Kernel.Extensions;

internal class RedisCacheServiceStub : ICacheService
{
    private readonly IDistributedCache? _cache;

    public RedisCacheServiceStub(IDistributedCache? cache = null)
    {
        _cache = cache;
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        if (_cache is null) return default;

        var value = await _cache.GetStringAsync(key, cancellationToken);
        if (string.IsNullOrEmpty(value)) return default;

        return System.Text.Json.JsonSerializer.Deserialize<T>(value);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null, CancellationToken cancellationToken = default)
    {
        if (_cache is null) return;

        var serialized = System.Text.Json.JsonSerializer.Serialize(value);
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiry ?? TimeSpan.FromMinutes(30)
        };
        await _cache.SetStringAsync(key, serialized, options, cancellationToken);
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        if (_cache is null) return;
        await _cache.RemoveAsync(key, cancellationToken);
    }

    public async Task RemoveByPatternAsync(string pattern, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default)
    {
        if (_cache is null) return false;
        var value = await _cache.GetStringAsync(key, cancellationToken);
        return !string.IsNullOrEmpty(value);
    }
}

internal class MessageBrokerStub : IMessageBroker
{
    public async Task PublishAsync<T>(T message, string? topic = null, CancellationToken cancellationToken = default) where T : class
    {
        await Task.CompletedTask;
    }

    public async Task SendAsync<T>(T message, string queue, CancellationToken cancellationToken = default) where T : class
    {
        await Task.CompletedTask;
    }

    public async Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request, string queue, CancellationToken cancellationToken = default)
        where TRequest : class
        where TResponse : class
    {
        await Task.CompletedTask;
        return Activator.CreateInstance<TResponse>();
    }
}

internal class DomainEventDispatcherStub : IDomainEventDispatcher
{
    public async Task DispatchAsync(IReadOnlyList<MediatR.INotification> domainEvents, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }
}
