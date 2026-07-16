using System.Reflection;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using HRMS.Services.ProjectTask.Application.Behaviours;
using HRMS.Services.ProjectTask.Application.Mappings;

namespace HRMS.Services.ProjectTask.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ProjectTaskMappingProfile>();
        });
        config.AssertConfigurationIsValid();
        services.AddSingleton<IMapper>(new Mapper(config));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
}
