using System.Net;
using System.Text.Json;
using HRMS.Shared.Kernel.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace HRMS.Shared.Caching.Services;

public class RedisCacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;
    private readonly IConnectionMultiplexer? _connectionMultiplexer;
    private const string KeyPrefix = "hrms:";
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };

    public RedisCacheService(IDistributedCache distributedCache, IConnectionMultiplexer? connectionMultiplexer = null)
    {
        _distributedCache = distributedCache;
        _connectionMultiplexer = connectionMultiplexer;
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var prefixedKey = GetPrefixedKey(key);
        var value = await _distributedCache.GetStringAsync(prefixedKey, cancellationToken);

        if (string.IsNullOrEmpty(value))
            return default;

        return JsonSerializer.Deserialize<T>(value, JsonOptions);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null, CancellationToken cancellationToken = default)
    {
        var prefixedKey = GetPrefixedKey(key);
        var serialized = JsonSerializer.Serialize(value, JsonOptions);

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiry ?? TimeSpan.FromMinutes(30)
        };

        await _distributedCache.SetStringAsync(prefixedKey, serialized, options, cancellationToken);
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        var prefixedKey = GetPrefixedKey(key);
        await _distributedCache.RemoveAsync(prefixedKey, cancellationToken);
    }

    public async Task RemoveByPatternAsync(string pattern, CancellationToken cancellationToken = default)
    {
        if (_connectionMultiplexer is null || !_connectionMultiplexer.IsConnected)
            return;

        var server = _connectionMultiplexer.GetServer(_connectionMultiplexer.GetEndPoints().First());
        var prefixedPattern = GetPrefixedKey(pattern);
        var keys = server.Keys(pattern: prefixedPattern);

        var database = _connectionMultiplexer.GetDatabase();
        var tasks = new List<Task>();

        foreach (var key in keys)
        {
            tasks.Add(database.KeyDeleteAsync(key));
        }

        await Task.WhenAll(tasks);
    }

    public async Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default)
    {
        if (_connectionMultiplexer is not null && _connectionMultiplexer.IsConnected)
        {
            var prefixedKey = GetPrefixedKey(key);
            var database = _connectionMultiplexer.GetDatabase();
            return await database.KeyExistsAsync(prefixedKey);
        }

        var value = await _distributedCache.GetStringAsync(GetPrefixedKey(key), cancellationToken);
        return !string.IsNullOrEmpty(value);
    }

    public async Task<T?> GetOrSetAsync<T>(
        string key,
        Func<Task<T>> factory,
        TimeSpan? expiry = null,
        CancellationToken cancellationToken = default)
    {
        var cached = await GetAsync<T>(key, cancellationToken);
        if (cached is not null)
            return cached;

        var value = await factory();
        await SetAsync(key, value, expiry, cancellationToken);
        return value;
    }

    public async Task<long> IncrementAsync(string key, long value = 1, CancellationToken cancellationToken = default)
    {
        if (_connectionMultiplexer is not null && _connectionMultiplexer.IsConnected)
        {
            var prefixedKey = GetPrefixedKey(key);
            var database = _connectionMultiplexer.GetDatabase();
            return await database.StringIncrementAsync(prefixedKey, value);
        }

        var current = await GetAsync<long>(key, cancellationToken);
        var newVal = current + value;
        await SetAsync(key, newVal, cancellationToken: cancellationToken);
        return newVal;
    }

    private static string GetPrefixedKey(string key)
    {
        return key.StartsWith(KeyPrefix) ? key : $"{KeyPrefix}{key}";
    }
}
