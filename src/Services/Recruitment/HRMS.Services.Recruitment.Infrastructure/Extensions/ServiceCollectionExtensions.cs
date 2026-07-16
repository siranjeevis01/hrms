using HRMS.Services.Recruitment.Application.Interfaces;
using HRMS.Services.Recruitment.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRMS.Services.Recruitment.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RecruitmentDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("RecruitmentDb"),
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(RecruitmentDbContext).Assembly.FullName);
                    npgsqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
                }));

        services.AddScoped<IRecruitmentDbContext>(provider => provider.GetRequiredService<RecruitmentDbContext>());

        return services;
    }
}
