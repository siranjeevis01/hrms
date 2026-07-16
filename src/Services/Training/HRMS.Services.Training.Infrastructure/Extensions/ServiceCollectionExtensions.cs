using HRMS.Services.Training.Application.Interfaces;
using HRMS.Services.Training.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRMS.Services.Training.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TrainingDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("TrainingDb"),
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(TrainingDbContext).Assembly.FullName);
                    npgsqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
                }));

        services.AddScoped<ITrainingDbContext>(provider => provider.GetRequiredService<TrainingDbContext>());

        return services;
    }
}
