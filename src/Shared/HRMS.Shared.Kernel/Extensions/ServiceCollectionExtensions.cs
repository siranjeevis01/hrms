using FluentValidation;
using HRMS.Shared.Kernel.Common;
using HRMS.Shared.Kernel.Filters;
using HRMS.Shared.Kernel.Interfaces;
using HRMS.Shared.Kernel.Middleware;
using HRMS.Shared.Kernel.Tenancy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace HRMS.Shared.Kernel.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSharedKernel(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddValidatorsFromAssemblyContaining<PaginationRequest>();

        services.AddSingleton<ICacheService, RedisCacheServiceStub>();
        services.AddSingleton<IMessageBroker, MessageBrokerStub>();
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcherStub>();

        return services;
    }

    public static IServiceCollection AddSharedKernelWithRedis(
        this IServiceCollection services,
        string redisConnectionString)
    {
        services.AddSharedKernel();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConnectionString;
            options.InstanceName = "HRMS_";
        });

        return services;
    }

    public static IApplicationBuilder UseSharedKernel(this IApplicationBuilder app)
    {
        app.UseMiddleware<CorrelationIdMiddleware>();
        app.UseMiddleware<RequestLoggingMiddleware>();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseMiddleware<TenantMiddleware>();
        app.UseMiddleware<AuditLoggingMiddleware>();

        return app;
    }
}
