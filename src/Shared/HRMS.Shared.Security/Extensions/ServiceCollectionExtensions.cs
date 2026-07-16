using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using HRMS.Shared.Kernel.Enums;
using HRMS.Shared.Security.Authentication;
using HRMS.Shared.Security.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HRMS.Shared.Security.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSecurity(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var tokenConfig = new TokenValidationConfig();
        configuration.GetSection("Jwt").Bind(tokenConfig);
        services.Configure<TokenValidationConfig>(configuration.GetSection("Jwt"));

        var firebaseConfig = new FirebaseAuthConfig();
        configuration.GetSection("Firebase").Bind(firebaseConfig);
        services.Configure<FirebaseAuthConfig>(configuration.GetSection("Firebase"));

        InitializeFirebase(firebaseConfig);

        var key = Encoding.UTF8.GetBytes(tokenConfig.SecretKey);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = !IsDevelopment();
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = tokenConfig.Issuer,
                ValidateAudience = true,
                ValidAudience = tokenConfig.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(1),
                RequireExpirationTime = true,
                RoleClaimType = "role",
                NameClaimType = "sub"
            };
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception is SecurityTokenExpiredException)
                    {
                        context.Response.Headers["Token-Expired"] = "true";
                    }
                    return Task.CompletedTask;
                },
                OnTokenValidated = async context =>
                {
                    var firebaseUser = context.Principal;
                    if (firebaseUser is not null && firebaseUser.Identity?.IsAuthenticated == true)
                    {
                        var userId = firebaseUser.FindFirst("sub")?.Value;
                        if (!string.IsNullOrEmpty(userId))
                        {
                            var tenantClaim = firebaseUser.FindFirst("tenant_id");
                            if (tenantClaim is null)
                            {
                                context.Fail("Tenant claim is required");
                                return;
                            }
                        }
                    }
                }
            };
        });

        services.AddAuthorization(options =>
        {
            var allPermissions = Enum.GetNames<UserRole>()
                .SelectMany(r => GetPermissionsForRole(r))
                .Distinct();

            foreach (var permission in allPermissions)
            {
                options.AddPolicy(permission, policy =>
                    policy.Requirements.Add(new PermissionRequirement(permission)));
            }

            options.AddPolicy("SuperAdmin", policy =>
                policy.RequireRole(UserRole.SuperAdmin.ToString()));

            options.AddPolicy("HRAdmin", policy =>
                policy.RequireRole(
                    UserRole.SuperAdmin.ToString(),
                    UserRole.HRAdmin.ToString()));

            options.AddPolicy("Manager", policy =>
                policy.RequireRole(
                    UserRole.SuperAdmin.ToString(),
                    UserRole.HRAdmin.ToString(),
                    UserRole.HRManager.ToString(),
                    UserRole.Manager.ToString()));
        });

        services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
        services.AddSingleton<IRolePermissionProvider, RolePermissionProvider>();
        services.AddSingleton<JwtTokenService>();

        return services;
    }

    private static void InitializeFirebase(FirebaseAuthConfig config)
    {
        if (FirebaseApp.DefaultInstance is not null) return;

        if (!string.IsNullOrEmpty(config.ServiceAccountKeyJson))
        {
            var credential = GoogleCredential.FromJson(config.ServiceAccountKeyJson);
            FirebaseApp.Create(new AppOptions
            {
                Credential = credential,
                ProjectId = config.ProjectId
            });
        }
        else if (!string.IsNullOrEmpty(config.ServiceAccountKeyPath) && File.Exists(config.ServiceAccountKeyPath))
        {
            var credential = GoogleCredential.FromFile(config.ServiceAccountKeyPath);
            FirebaseApp.Create(new AppOptions
            {
                Credential = credential,
                ProjectId = config.ProjectId
            });
        }
    }

    private static bool IsDevelopment()
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        return string.Equals(environment, "Development", StringComparison.OrdinalIgnoreCase);
    }

    private static List<string> GetPermissionsForRole(string role)
    {
        return role switch
        {
            "SuperAdmin" => new List<string>
            {
                "employees.view", "employees.create", "employees.edit", "employees.delete",
                "departments.view", "departments.create", "departments.edit", "departments.delete",
                "attendance.view", "attendance.manage",
                "leave.view", "leave.approve", "leave.manage",
                "payroll.view", "payroll.process", "payroll.manage",
                "reports.view", "reports.generate", "reports.export",
                "settings.view", "settings.manage",
                "users.view", "users.create", "users.edit", "users.delete",
                "roles.manage", "tenants.manage"
            },
            "HRAdmin" => new List<string>
            {
                "employees.view", "employees.create", "employees.edit", "employees.delete",
                "departments.view", "departments.create", "departments.edit",
                "attendance.view", "attendance.manage",
                "leave.view", "leave.approve", "leave.manage",
                "payroll.view", "payroll.process",
                "reports.view", "reports.generate", "reports.export",
                "settings.view", "settings.manage",
                "users.view", "users.create", "users.edit"
            },
            "HRManager" => new List<string>
            {
                "employees.view", "employees.create", "employees.edit",
                "departments.view", "departments.create", "departments.edit",
                "attendance.view", "attendance.manage",
                "leave.view", "leave.approve",
                "payroll.view",
                "reports.view", "reports.generate",
                "users.view", "users.create"
            },
            "Manager" => new List<string>
            {
                "employees.view",
                "departments.view",
                "attendance.view",
                "leave.view", "leave.approve",
                "reports.view",
                "users.view"
            },
            "Employee" => new List<string>
            {
                "employees.view",
                "attendance.view",
                "leave.view", "leave.apply",
                "reports.view"
            },
            "ReadOnly" => new List<string>
            {
                "employees.view",
                "departments.view",
                "attendance.view",
                "leave.view",
                "reports.view"
            },
            _ => new List<string>()
        };
    }
}
