using System.Reflection;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using HRMS.Services.Recruitment.Application.Behaviours;

namespace HRMS.Services.Recruitment.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Array.Empty<Assembly>());
        services.AddSingleton<IMapper>(sp => 
            new MapperConfiguration(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly())).CreateMapper());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
}
