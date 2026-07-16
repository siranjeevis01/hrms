using HRMS.Services.Audit.Application.Interfaces;
using HRMS.Services.Audit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRMS.Services.Audit.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AuditDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("AuditDb"),
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(AuditDbContext).Assembly.FullName);
                    npgsqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
                }));

        services.AddScoped<IAuditDbContext>(provider => provider.GetRequiredService<AuditDbContext>());

        return services;
    }
}
