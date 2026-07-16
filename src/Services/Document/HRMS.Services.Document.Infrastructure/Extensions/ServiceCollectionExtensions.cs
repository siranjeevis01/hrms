using HRMS.Services.Document.Application.Interfaces;
using HRMS.Services.Document.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRMS.Services.Document.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DocumentDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DocumentDb"),
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(DocumentDbContext).Assembly.FullName);
                    npgsqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
                }));

        services.AddScoped<IDocumentDbContext>(provider => provider.GetRequiredService<DocumentDbContext>());

        return services;
    }
}
