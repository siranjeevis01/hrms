using HRMS.Services.Dashboard.Application.Interfaces;
using HRMS.Services.Dashboard.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRMS.Services.Dashboard.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DashboardDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DashboardDb"),
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(DashboardDbContext).Assembly.FullName);
                    npgsqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
                }));

        services.AddScoped<IDashboardDbContext>(provider => provider.GetRequiredService<DashboardDbContext>());

        return services;
    }
}
