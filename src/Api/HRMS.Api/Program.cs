using System.IO.Compression;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation;
using HRMS.Api;
using HRMS.Services.Identity.Application.Interfaces;
using HRMS.Services.Identity.Infrastructure.Extensions;
using HRMS.Shared.Kernel.Common;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Seq(builder.Configuration["Seq:Url"] ?? "http://localhost:5341")
    .CreateLogger();

try
{
    Log.Information("Starting HRMS Pro API...");

    builder.Host.UseSerilog();

    builder.Services.AddControllers()
        .AddApplicationPart(typeof(HRMS.Services.Identity.API.Controllers.AuthController).Assembly)
        .AddApplicationPart(typeof(HRMS.Services.Attendance.API.Controllers.AttendanceController).Assembly)
        .AddApplicationPart(typeof(HRMS.Services.Audit.API.Controllers.AuditLogsController).Assembly)
        .AddApplicationPart(typeof(HRMS.Services.Chat.API.Controllers.ChannelsController).Assembly)
        .AddApplicationPart(typeof(HRMS.Services.Dashboard.API.Controllers.DashboardsController).Assembly)
        .AddApplicationPart(typeof(HRMS.Services.Document.API.Controllers.DocumentsController).Assembly)
        .AddApplicationPart(typeof(HRMS.Services.Employee.API.Controllers.EmployeesController).Assembly)
        .AddApplicationPart(typeof(HRMS.Services.Expense.API.Controllers.ExpenseClaimsController).Assembly)
        .AddApplicationPart(typeof(HRMS.Services.Helpdesk.API.Controllers.TicketsController).Assembly)
        .AddApplicationPart(typeof(HRMS.Services.Leave.API.Controllers.LeaveApplicationsController).Assembly)
        .AddApplicationPart(typeof(HRMS.Services.Notification.API.Controllers.NotificationsController).Assembly)
        .AddApplicationPart(typeof(HRMS.Services.Organization.API.Controllers.CompanyController).Assembly)
        .AddApplicationPart(typeof(HRMS.Services.Payroll.API.Controllers.PayrollController).Assembly)
        .AddApplicationPart(typeof(HRMS.Services.Performance.API.Controllers.PerformanceReviewsController).Assembly)
        .AddApplicationPart(typeof(HRMS.Services.ProjectTask.API.Controllers.ProjectsController).Assembly)
        .AddApplicationPart(typeof(HRMS.Services.Recruitment.API.Controllers.CandidatesController).Assembly)
        .AddApplicationPart(typeof(HRMS.Services.Report.API.Controllers.ReportTemplatesController).Assembly)
        .AddApplicationPart(typeof(HRMS.Services.Training.API.Controllers.CoursesController).Assembly)
        .AddApplicationPart(typeof(HRMS.Services.Travel.API.Controllers.TravelRequestsController).Assembly)
        .AddApplicationPart(typeof(HRMS.Services.Workflow.API.Controllers.WorkflowDefinitionsController).Assembly)
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

    // ── Module Application Services (MediatR, FluentValidation, AutoMapper) ──
    var applicationAssemblies = new[]
    {
        typeof(HRMS.Services.Identity.Application.DependencyInjection).Assembly,
        typeof(HRMS.Services.Employee.Application.DependencyInjection).Assembly,
        typeof(HRMS.Services.Organization.Application.DependencyInjection).Assembly,
        typeof(HRMS.Services.Attendance.Application.DependencyInjection).Assembly,
        typeof(HRMS.Services.Leave.Application.DependencyInjection).Assembly,
        typeof(HRMS.Services.Payroll.Application.DependencyInjection).Assembly,
        typeof(HRMS.Services.Notification.Application.DependencyInjection).Assembly,
        typeof(HRMS.Services.Recruitment.Application.DependencyInjection).Assembly,
        typeof(HRMS.Services.ProjectTask.Application.DependencyInjection).Assembly,
        typeof(HRMS.Services.Performance.Application.DependencyInjection).Assembly,
        typeof(HRMS.Services.Training.Application.DependencyInjection).Assembly,
        typeof(HRMS.Services.Audit.Application.DependencyInjection).Assembly,
        typeof(HRMS.Services.Report.Application.DependencyInjection).Assembly,
        typeof(HRMS.Services.Dashboard.Application.DependencyInjection).Assembly,
        typeof(HRMS.Services.Expense.Application.DependencyInjection).Assembly,
        typeof(HRMS.Services.Travel.Application.DependencyInjection).Assembly,
        typeof(HRMS.Services.Helpdesk.Application.DependencyInjection).Assembly,
        typeof(HRMS.Services.Chat.Application.DependencyInjection).Assembly,
        typeof(HRMS.Services.Document.Application.DependencyInjection).Assembly,
        typeof(HRMS.Services.Workflow.Application.DependencyInjection).Assembly,
    };

    builder.Services.AddMediatR(cfg =>
    {
        foreach (var assembly in applicationAssemblies)
            cfg.RegisterServicesFromAssembly(assembly);
    });

    foreach (var assembly in applicationAssemblies)
        builder.Services.AddValidatorsFromAssembly(assembly);

    // ── Identity Infrastructure Services (for monolith) ──
    builder.Services.AddScoped<HRMS.Services.Identity.Infrastructure.Services.PasswordHasher>();
    builder.Services.AddScoped<HRMS.Services.Identity.Application.Interfaces.IPasswordHasher, HRMS.Services.Identity.Infrastructure.Services.PasswordHasherAdapter>();
    builder.Services.AddScoped<HRMS.Services.Identity.Infrastructure.Services.TokenService>();
    builder.Services.AddScoped<HRMS.Services.Identity.Application.Interfaces.ITokenService, HRMS.Services.Identity.Infrastructure.Services.ApplicationTokenServiceAdapter>();
    builder.Services.AddScoped<HRMS.Services.Identity.Infrastructure.Services.TotpService>();
    builder.Services.AddScoped<HRMS.Services.Identity.Application.Interfaces.ITotpService, HRMS.Services.Identity.Infrastructure.Services.TotpServiceAdapter>();
    builder.Services.Configure<HRMS.Services.Identity.Infrastructure.Services.JwtSettings>(builder.Configuration.GetSection("Jwt"));
    builder.Services.Configure<HRMS.Services.Identity.Infrastructure.Services.FirebaseAuthSettings>(builder.Configuration.GetSection("Firebase"));

    // ── Email Service (SMTP) ──
    builder.Services.Configure<HRMS.Services.Identity.Infrastructure.Services.SmtpOptions>(builder.Configuration.GetSection(HRMS.Services.Identity.Infrastructure.Services.SmtpOptions.SectionName));
    builder.Services.AddScoped<HRMS.Services.Identity.Application.Interfaces.IEmailService, HRMS.Services.Identity.Infrastructure.Services.SmtpEmailService>();

    // ── Notification Service (monolith) ──
    builder.Services.Configure<HRMS.Api.Services.SmtpSettings>(builder.Configuration.GetSection(HRMS.Api.Services.SmtpSettings.SectionName));
    builder.Services.AddScoped<HRMS.Shared.Kernel.Interfaces.INotificationService, HRMS.Api.Services.MonolithNotificationService>();

    builder.Services.AddScoped<IIdentityDbContext>(sp =>
        new IdentityDbContextAdapter(sp.GetRequiredService<HrmsDbContext>()));

    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "HRMS Pro API",
            Version = "v1",
            Description = "Human Resource Management System - Consolidated API"
        });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header. Example: \"Bearer {token}\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    });

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrWhiteSpace(connectionString))
        connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
    if (string.IsNullOrWhiteSpace(connectionString))
        throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

    Log.Information("Connection string starts with: {Prefix}", connectionString.Length > 20 ? connectionString.Substring(0, 20) + "..." : connectionString);

    builder.Services.AddDbContext<HrmsDbContext>(options =>
        options.UseNpgsql(connectionString, npgsqlOptions =>
        {
            npgsqlOptions.MigrationsAssembly(typeof(HrmsDbContext).Assembly.FullName);
            npgsqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
        }));

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Authority = builder.Configuration["Jwt:Authority"];
            options.Audience = builder.Configuration["Jwt:Audience"];
            options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = builder.Configuration["Jwt:Audience"],
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes(
                        builder.Configuration["Jwt:SecretKey"] ?? "HRMS-Dev-Secret-Key-Change-In-Production-2024!"))
            };
        });

    builder.Services.AddAuthorization();

    var corsOriginsRaw = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
    if (corsOriginsRaw is null || corsOriginsRaw.Length == 0)
    {
        var rawValue = builder.Configuration["Cors:AllowedOrigins"];
        if (!string.IsNullOrWhiteSpace(rawValue))
        {
            try
            {
                corsOriginsRaw = JsonSerializer.Deserialize<string[]>(rawValue, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch
            {
                corsOriginsRaw = rawValue.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            }
        }
    }
    var corsOrigins = corsOriginsRaw is { Length: > 0 }
        ? corsOriginsRaw
        : new[] { "https://courageous-rolypoly-c81b64.netlify.app", "https://hrms-pro.netlify.app", "http://localhost:4200" };
    var allowAllOrigins = corsOrigins.Any(o => o == "*" || o == "https://*");

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowConfigured", policy =>
        {
            if (allowAllOrigins)
            {
                policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }
            else
            {
                policy.WithOrigins(corsOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithExposedHeaders("X-Pagination");
            }
        });

        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    });

    builder.Services.AddResponseCompression(options =>
    {
        options.EnableForHttps = true;
        options.Providers.Add<BrotliCompressionProvider>();
        options.Providers.Add<GzipCompressionProvider>();
        options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
            new[] { "application/json", "text/plain", "text/css", "application/javascript" });
    });

    builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
    {
        options.Level = CompressionLevel.Fastest;
    });

    builder.Services.Configure<GzipCompressionProviderOptions>(options =>
    {
        options.Level = CompressionLevel.Fastest;
    });

    builder.Services.AddHealthChecks()
        .AddNpgSql(connectionString, name: "postgresql", tags: new[] { "db", "ready" });

    builder.Services.AddDistributedMemoryCache();
    builder.Services.AddSession();

    builder.Services.Configure<ForwardedHeadersOptions>(options =>
    {
        options.ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor |
                                   Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto;
    });

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HRMS Pro API v1"));
    }

    app.UseForwardedHeaders();
    app.UseResponseCompression();
    if (!app.Environment.IsEnvironment("Production"))
        app.UseHttpsRedirection();

    var corsPolicy = app.Environment.IsDevelopment() ? "AllowAll" : "AllowConfigured";
    app.UseCors(corsPolicy);

    if (!app.Environment.IsDevelopment())
    {
        app.UseHsts();
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var exceptionHandlerFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
                var exception = exceptionHandlerFeature?.Error;
                Log.Error(exception, "Unhandled exception on {Method} {Path}", context.Request.Method, context.Request.Path);

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var inner = exception?.InnerException;
                var response = System.Text.Json.JsonSerializer.Serialize(new
                {
                    statusCode = 500,
                    message = "An unexpected error occurred.",
                    detail = exception?.Message,
                    innerDetail = inner?.Message,
                    innerType = inner?.GetType().Name
                }, new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase });
                await context.Response.WriteAsync(response);
            });
        });
    }

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseSession();

    app.MapGet("/", () => Results.Json(new
    {
        service = "HRMS Pro API",
        version = "1.0.0",
        status = "running",
        health = "/health/live"
    }));
    app.MapControllers();
    app.MapHealthChecks("/health/live", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
    {
        Predicate = _ => false
    });
    app.MapHealthChecks("/health/ready", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
    {
        Predicate = check => check.Tags.Contains("ready"),
        ResponseWriter = async (context, report) =>
        {
            context.Response.ContentType = "application/json";
            var result = JsonSerializer.Serialize(new
            {
                status = report.Status.ToString(),
                checks = report.Entries.Select(e => new
                {
                    name = e.Key,
                    status = e.Value.Status.ToString(),
                    duration = e.Value.Duration.ToString(),
                    description = e.Value.Description
                })
            });
            await context.Response.WriteAsync(result);
        }
    });

    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<HrmsDbContext>();
        if (db.Database.GetPendingMigrations().Any())
        {
            Log.Information("Applying database migrations...");
            db.Database.Migrate();
            Log.Information("Migrations applied successfully.");
        }

        var conn = db.Database.GetDbConnection();
        await conn.OpenAsync();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            DO $$ BEGIN
                IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name = 'ApplicationUsers' AND column_name = 'PasswordHash') THEN
                    ALTER TABLE ""ApplicationUsers"" ADD COLUMN ""PasswordHash"" text;
                END IF;
            END $$;";
        await cmd.ExecuteNonQueryAsync();
        await conn.CloseAsync();
    }

    Log.Information("HRMS Pro API started successfully on {Urls}", builder.Configuration["Urls"] ?? "default");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
