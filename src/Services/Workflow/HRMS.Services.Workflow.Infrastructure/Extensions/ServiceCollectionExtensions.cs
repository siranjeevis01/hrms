using HRMS.Services.Workflow.Application.Interfaces;
using HRMS.Services.Workflow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRMS.Services.Workflow.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WorkflowDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("WorkflowDb"),
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(WorkflowDbContext).Assembly.FullName);
                    npgsqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
                }));

        services.AddScoped<IWorkflowDbContext>(provider => provider.GetRequiredService<WorkflowDbContext>());

        return services;
    }
}
