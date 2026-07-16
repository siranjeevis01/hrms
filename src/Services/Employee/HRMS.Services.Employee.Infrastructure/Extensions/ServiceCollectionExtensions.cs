using HRMS.Services.Employee.Application.Interfaces;
using HRMS.Services.Employee.Infrastructure.Persistence;
using HRMS.Services.Employee.Infrastructure.Repositories;
using HRMS.Services.Employee.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRMS.Services.Employee.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EmployeeDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("EmployeeDb"),
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(EmployeeDbContext).Assembly.FullName);
                    npgsqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
                }));

        services.AddScoped<IEmployeeDbContext>(provider => provider.GetRequiredService<EmployeeDbContext>());
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        return services;
    }
}
