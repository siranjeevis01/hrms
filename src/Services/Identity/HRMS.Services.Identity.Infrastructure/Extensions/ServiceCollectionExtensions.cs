using HRMS.Services.Identity.Infrastructure.Persistence;
using HRMS.Services.Identity.Infrastructure.Repositories;
using HRMS.Services.Identity.Infrastructure.Repositories.Interfaces;
using HRMS.Services.Identity.Infrastructure.Services;
using HRMS.Shared.Kernel.Interfaces;
using HRMS.Shared.Persistence;
using HRMS.Shared.Persistence.Interfaces;
using HRMS.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace HRMS.Services.Identity.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<IdentityDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly(typeof(IdentityDbContext).Assembly.FullName);
                npgsqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
            });
        });

        services.AddScoped<ApplicationDbContext>(sp =>
            sp.GetRequiredService<IdentityDbContext>());

        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
        services.Configure<FirebaseAuthSettings>(configuration.GetSection("Firebase"));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ITotpService, TotpService>();
        services.AddScoped<IFirebaseAuthService, FirebaseAuthService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IAuditService, AuditLogService>();

        services.AddHttpContextAccessor();

        var redisConnectionString = configuration.GetConnectionString("Redis");
        if (!string.IsNullOrEmpty(redisConnectionString))
        {
            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var configurationOptions = ConfigurationOptions.Parse(redisConnectionString);
                configurationOptions.AbortOnConnectFail = false;
                configurationOptions.ConnectRetry = 3;
                configurationOptions.ConnectTimeout = 5000;
                configurationOptions.SyncTimeout = 5000;
                return ConnectionMultiplexer.Connect(configurationOptions);
            });

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
                options.InstanceName = "hrms:identity:";
            });
        }

        return services;
    }
}
