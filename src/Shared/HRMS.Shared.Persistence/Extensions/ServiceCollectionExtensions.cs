using HRMS.Shared.Kernel.Interfaces;
using HRMS.Shared.Persistence;
using HRMS.Shared.Persistence.Interfaces;
using HRMS.Shared.Persistence.Interceptors;
using HRMS.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HRMS.Shared.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddSingleton<AuditableEntityInterceptor>();
        services.AddSingleton<SoftDeleteInterceptor>();
        services.AddSingleton<TenantInterceptor>();

        services.AddScoped<DbContextOptions<ApplicationDbContext>>(sp =>
        {
            var auditableInterceptor = sp.GetRequiredService<AuditableEntityInterceptor>();
            var softDeleteInterceptor = sp.GetRequiredService<SoftDeleteInterceptor>();
            var tenantInterceptor = sp.GetRequiredService<TenantInterceptor>();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder
                .UseNpgsql(connectionString, npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    npgsqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
                })
                .AddInterceptors(auditableInterceptor, softDeleteInterceptor, tenantInterceptor)
                .EnableSensitiveDataLogging(false)
                .EnableDetailedErrors(false);

            return optionsBuilder.Options;
        });

        services.AddScoped<ApplicationDbContext>();

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.Scan(scan => scan
            .FromAssemblyOf<ApplicationDbContext>()
            .AddClasses(classes => classes
                .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }

    public static IServiceCollection AddPersistence<TContext>(
        this IServiceCollection services,
        string connectionString)
        where TContext : ApplicationDbContext
    {
        services.AddSingleton<AuditableEntityInterceptor>();
        services.AddSingleton<SoftDeleteInterceptor>();
        services.AddSingleton<TenantInterceptor>();

        services.AddScoped<DbContextOptions<TContext>>(sp =>
        {
            var auditableInterceptor = sp.GetRequiredService<AuditableEntityInterceptor>();
            var softDeleteInterceptor = sp.GetRequiredService<SoftDeleteInterceptor>();
            var tenantInterceptor = sp.GetRequiredService<TenantInterceptor>();

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();
            optionsBuilder
                .UseNpgsql(connectionString, npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(TContext).Assembly.FullName);
                    npgsqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
                })
                .AddInterceptors(auditableInterceptor, softDeleteInterceptor, tenantInterceptor)
                .EnableSensitiveDataLogging(false)
                .EnableDetailedErrors(false);

            return optionsBuilder.Options;
        });

        services.AddScoped<TContext>();
        services.AddScoped<ApplicationDbContext>(sp => sp.GetRequiredService<TContext>());

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.Scan(scan => scan
            .FromAssemblyOf<TContext>()
            .AddClasses(classes => classes
                .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}
