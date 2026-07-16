using System.Text;
using System.Threading.RateLimiting;
using HRMS.ApiGateway.HealthChecks;
using HRMS.ApiGateway.Middleware;
using HRMS.ApiGateway.Transforms;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .ReadFrom.Services(builder.Services)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithEnvironmentName()
    .Enrich.WithProperty("Application", "HRMS.ApiGateway")
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{Level:u3}] [{CorrelationId}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.Seq(
        configuration["Seq:ServerUrl"] ?? "http://localhost:5341",
        apiKey: configuration["Seq:ApiKey"])
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowConfiguredOrigins", policy =>
    {
        var origins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
        policy
            .WithOrigins(origins)
            .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE", "OPTIONS")
            .WithHeaders("Authorization", "Content-Type", "X-Correlation-Id", "X-Request-Id", "Accept", "Origin")
            .AllowCredentials()
            .SetPreflightMaxAge(TimeSpan.FromMinutes(10));
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = configuration["Jwt:Authority"];
        options.Audience = configuration["Jwt:Audience"];
        options.RequireHttpsMetadata = bool.TryParse(configuration["Jwt:RequireHttps"], out var requireHttps) && requireHttps;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = configuration["Jwt:Audience"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.FromSeconds(30)
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Log.Warning("JWT authentication failed: {Error}", context.Exception.Message);
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Log.Debug("JWT token validated for {Subject}", context.Principal?.Identity?.Name);
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddFixedWindowLimiter("global", opt =>
    {
        opt.PermitLimit = int.TryParse(configuration["RateLimiting:Global:PermitLimit"], out var pl) ? pl : 200;
        opt.Window = TimeSpan.FromSeconds(int.TryParse(configuration["RateLimiting:Global:WindowSeconds"], out var ws) ? ws : 60);
        opt.QueueLimit = int.TryParse(configuration["RateLimiting:Global:QueueLimit"], out var ql) ? ql : 10;
    });

    options.OnRejected = async (context, ct) =>
    {
        context.HttpContext.Response.Headers.RetryAfter = "60";
        await context.HttpContext.Response.WriteAsync(
            "{\"error\":\"rate_limit_exceeded\",\"message\":\"Too many requests. Please try again later.\"}",
            ct);
    };
});

builder.Services.AddHealthChecks()
    .AddCheck<GatewayHealthCheck>(
        "gateway-downstream",
        failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Degraded,
        tags: ["ready"]);

builder.Services.Configure<GatewayHealthCheckOptions>(
    configuration.GetSection("HealthChecks"));

builder.Services.AddHttpClient("HealthCheck");

builder.Services.AddReverseProxy()
    .LoadFromConfig(configuration.GetSection("ReverseProxy"));

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "HRMS Pro API Gateway",
        Version = "v1",
        Description = "Enterprise HRMS API Gateway powered by YARP"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token"
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

if (bool.TryParse(configuration["OpenTelemetry:Enabled"], out var otelEnabled) && otelEnabled)
{
    builder.Services.AddOpenTelemetry()
        .WithTracing(tracing =>
        {
            tracing
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddSource("HRMS.ApiGateway")
                .AddOtlpExporter(options =>
                {
                    options.Endpoint = new Uri(
                        configuration["OpenTelemetry:OtlpEndpoint"] ?? "http://localhost:4317");
                });
        });
}

builder.Services.AddCors();

var app = builder.Build();

app.UseMiddleware<RequestTimingMiddleware>();

app.UseResponseCompression();

app.UseCors("AllowConfiguredOrigins");

app.UseRateLimiter();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HRMS Pro API Gateway v1");
    c.RoutePrefix = "swagger";
});

app.MapReverseProxy(pipeline =>
{
    pipeline.Use(async (context, next) =>
    {
        context.Request.Headers.Append("X-Forwarded-By", "HRMS-Gateway");
        context.Request.Headers.Append("X-Gateway-Instance", Environment.MachineName);
        await next();
    });
});

app.MapHealthChecks("/health/ready", new()
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var payload = new
        {
            Status = report.Status.ToString(),
            Duration = report.TotalDuration.TotalMilliseconds,
            Checks = report.Entries.Select(e => new
            {
                Name = e.Key,
                Status = e.Value.Status.ToString(),
                Duration = e.Value.Duration.TotalMilliseconds,
                Description = e.Value.Description
            })
        };
        await context.Response.WriteAsJsonAsync(payload);
    },
    Predicate = check => check.Tags.Contains("ready")
});

app.MapHealthChecks("/health/live", new()
{
    ResponseWriter = async (context, _) =>
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new { Status = "Healthy", Timestamp = DateTime.UtcNow });
    },
    Predicate = _ => false
});

app.MapGet("/", () => Results.Ok(new
{
    Service = "HRMS Pro API Gateway",
    Version = "1.0.0",
    Status = "Running",
    Timestamp = DateTime.UtcNow
}));

try
{
    Log.Information("Starting HRMS API Gateway");
    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
