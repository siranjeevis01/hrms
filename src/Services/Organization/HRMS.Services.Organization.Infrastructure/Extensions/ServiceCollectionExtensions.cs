using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HRMS.Services.Organization.Application.Interfaces;
using HRMS.Services.Organization.Infrastructure.Persistence;
using HRMS.Services.Organization.Infrastructure.Repositories;

namespace HRMS.Services.Organization.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<OrganizationDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("OrganizationConnection"),
                b => b.MigrationsAssembly(typeof(OrganizationDbContext).Assembly.FullName)));

        services.AddScoped<IOrganizationDbContext>(provider =>
            provider.GetRequiredService<OrganizationDbContext>());

        services.AddScoped(typeof(IOrganizationRepository<>), typeof(OrganizationRepository<>));
        services.AddScoped<CompanyRepository>();
        services.AddScoped<BranchRepository>();
        services.AddScoped<DepartmentRepository>();

        return services;
    }
}
