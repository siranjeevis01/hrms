using HRMS.Services.Leave.Application.Interfaces;
using HRMS.Services.Leave.Infrastructure.Persistence;
using HRMS.Services.Leave.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRMS.Services.Leave.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLeaveInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LeaveDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("LeaveDb"),
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(LeaveDbContext).Assembly.FullName);
                    npgsqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
                }));

        services.AddScoped<ILeaveDbContext>(provider => provider.GetRequiredService<LeaveDbContext>());
        services.AddScoped<ILeaveRepository, LeaveRepository>();

        return services;
    }
}
