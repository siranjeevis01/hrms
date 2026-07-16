using HRMS.Services.Performance.Application.Interfaces;
using HRMS.Services.Performance.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRMS.Services.Performance.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PerformanceDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("PerformanceDb"),
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(PerformanceDbContext).Assembly.FullName);
                    npgsqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
                }));

        services.AddScoped<IPerformanceDbContext>(provider => provider.GetRequiredService<PerformanceDbContext>());

        return services;
    }
}
