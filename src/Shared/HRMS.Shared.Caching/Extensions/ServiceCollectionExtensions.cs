using HRMS.Shared.Caching.Services;
using HRMS.Shared.Kernel.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace HRMS.Shared.Caching.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCaching(
        this IServiceCollection services,
        string redisConnectionString,
        string instanceName = "hrms:")
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConnectionString;
            options.InstanceName = instanceName;
        });

        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var configuration = ConfigurationOptions.Parse(redisConnectionString);
            configuration.AbortOnConnectFail = false;
            configuration.ConnectRetry = 3;
            configuration.ConnectTimeout = 5000;
            configuration.SyncTimeout = 5000;
            return ConnectionMultiplexer.Connect(configuration);
        });

        services.AddScoped<ICacheService, RedisCacheService>();

        return services;
    }
}
