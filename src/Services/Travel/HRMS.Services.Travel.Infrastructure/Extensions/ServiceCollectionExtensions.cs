using HRMS.Services.Travel.Application.Interfaces;
using HRMS.Services.Travel.Infrastructure.Persistence;
using HRMS.Services.Travel.Infrastructure.Repositories;
using HRMS.Services.Travel.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRMS.Services.Travel.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TravelDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("TravelDb"),
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(TravelDbContext).Assembly.FullName);
                    npgsqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
                }));

        services.AddScoped<ITravelDbContext>(provider => provider.GetRequiredService<TravelDbContext>());
        services.AddScoped<ITravelRepository, TravelRepository>();

        return services;
    }
}
