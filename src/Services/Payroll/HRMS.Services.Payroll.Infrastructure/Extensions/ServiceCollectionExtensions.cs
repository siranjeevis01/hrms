using HRMS.Services.Payroll.Application.Interfaces;
using HRMS.Services.Payroll.Infrastructure.Persistence;
using HRMS.Services.Payroll.Infrastructure.Repositories;
using HRMS.Services.Payroll.Infrastructure.Repositories.Interfaces;
using HRMS.Services.Payroll.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRMS.Services.Payroll.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPayrollInfrastructure(this IServiceCollection services,
        IConfiguration configuration, string connectionStringName = "PayrollConnection")
    {
        var connectionString = configuration.GetConnectionString(connectionStringName);

        services.AddDbContext<PayrollDbContext>(options =>
            options.UseNpgsql(connectionString,
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(PayrollDbContext).Assembly.FullName);
                    npgsqlOptions.EnableRetryOnFailure(3);
                }));

        services.AddScoped<IPayrollDbContext>(provider => provider.GetRequiredService<PayrollDbContext>());
        services.AddScoped<IPayrollRepository, PayrollRepository>();

        services.AddScoped<TaxCalculator>();
        services.AddScoped<PfCalculator>();
        services.AddScoped<EsiCalculator>();
        services.AddScoped<PayslipGenerator>();

        return services;
    }
}
