using FluentValidation;
using HRMS.Services.Notification.Application.Commands.SendNotification;
using HRMS.Services.Notification.Application.Interfaces;
using HRMS.Services.Notification.Application.Mappings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HRMS.Services.Notification.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(NotificationMappingProfile).Assembly);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SendNotificationCommand).Assembly));
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        return services;
    }
}
