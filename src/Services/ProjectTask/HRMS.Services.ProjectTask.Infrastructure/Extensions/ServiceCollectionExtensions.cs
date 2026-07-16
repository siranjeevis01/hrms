using HRMS.Services.ProjectTask.Application.Interfaces;
using HRMS.Services.ProjectTask.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRMS.Services.ProjectTask.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProjectTaskDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("ProjectTaskDb"),
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(ProjectTaskDbContext).Assembly.FullName);
                    npgsqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
                }));

        services.AddScoped<IProjectTaskDbContext>(provider => provider.GetRequiredService<ProjectTaskDbContext>());

        return services;
    }
}
