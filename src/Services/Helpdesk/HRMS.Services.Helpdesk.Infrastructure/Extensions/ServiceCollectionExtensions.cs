using HRMS.Services.Helpdesk.Application.Interfaces;
using HRMS.Services.Helpdesk.Infrastructure.Persistence;
using HRMS.Services.Helpdesk.Infrastructure.Repositories;
using HRMS.Services.Helpdesk.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRMS.Services.Helpdesk.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<HelpdeskDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("HelpdeskDb"),
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(HelpdeskDbContext).Assembly.FullName);
                    npgsqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
                }));

        services.AddScoped<IHelpdeskDbContext>(provider => provider.GetRequiredService<HelpdeskDbContext>());
        services.AddScoped<IHelpdeskRepository, HelpdeskRepository>();

        return services;
    }
}
