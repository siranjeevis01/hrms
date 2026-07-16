using HRMS.Services.Attendance.Application.Interfaces;
using HRMS.Services.Attendance.Infrastructure.Persistence;
using HRMS.Services.Attendance.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRMS.Services.Attendance.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAttendanceInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AttendanceDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("AttendanceDb"),
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(AttendanceDbContext).Assembly.FullName);
                    npgsqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
                }));

        services.AddScoped<IAttendanceDbContext>(provider => provider.GetRequiredService<AttendanceDbContext>());
        services.AddScoped<IAttendanceRepository, AttendanceRepository>();

        return services;
    }
}
