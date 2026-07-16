using HRMS.Services.Expense.Application.Interfaces;
using HRMS.Services.Expense.Infrastructure.Persistence;
using HRMS.Services.Expense.Infrastructure.Repositories;
using HRMS.Services.Expense.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRMS.Services.Expense.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ExpenseDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("ExpenseDb"),
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(ExpenseDbContext).Assembly.FullName);
                    npgsqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
                }));

        services.AddScoped<IExpenseDbContext>(provider => provider.GetRequiredService<ExpenseDbContext>());
        services.AddScoped<IExpenseRepository, ExpenseRepository>();

        return services;
    }
}
