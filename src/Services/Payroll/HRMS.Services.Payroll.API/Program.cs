using HRMS.Services.Payroll.Application;
using HRMS.Services.Payroll.Infrastructure.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "HRMS Payroll Service", Version = "v1" });
});

builder.Services.AddPayrollApplication();
builder.Services.AddPayrollInfrastructure(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseSerilogRequestLogging();
app.MapControllers();

try
{
    Log.Information("Starting HRMS Payroll Service");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "HRMS Payroll Service terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
