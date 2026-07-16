using HRMS.Shared.Kernel.Interfaces;
using HRMS.Shared.Messaging.Interfaces;
using HRMS.Shared.Messaging.Services;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace HRMS.Shared.Messaging.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInMemoryMessaging(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.AddConsumers(typeof(ServiceCollectionExtensions).Assembly);

            x.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddScoped<IMessageBus, MassTransitMessageBus>();
        services.AddSingleton<IMessageBroker, MassTransitMessageBus>();

        return services;
    }

    /// <summary>
    /// For microservices deployment, add MassTransit.RabbitMQ package and implement:
    /// services.AddMessaging(rabbitMqHost, rabbitMqUsername, rabbitMqPassword);
    /// </summary>
}
