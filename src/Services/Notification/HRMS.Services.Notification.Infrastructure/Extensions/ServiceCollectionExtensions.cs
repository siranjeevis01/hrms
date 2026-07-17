using HRMS.Services.Notification.Application.Interfaces;
using HRMS.Services.Notification.Infrastructure.Persistence;
using HRMS.Services.Notification.Infrastructure.Repositories;
using HRMS.Services.Notification.Infrastructure.Repositories.Interfaces;
using HRMS.Services.Notification.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRMS.Services.Notification.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNotificationInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<NotificationDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("NotificationDb"),
                b => b.MigrationsAssembly(typeof(NotificationDbContext).Assembly.FullName)));

        services.AddScoped<INotificationDbContext>(provider =>
            provider.GetRequiredService<NotificationDbContext>());

        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<INotificationRenderer, NotificationRenderer>();

        return services;
    }
}
