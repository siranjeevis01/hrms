using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HRMS.Services.Notification.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationHealthCheckController : ControllerBase
{
    private readonly HealthCheckService _healthCheckService;

    public NotificationHealthCheckController(HealthCheckService healthCheckService)
    {
        _healthCheckService = healthCheckService;
    }

    [HttpGet]
    public async Task<IActionResult> Check()
    {
        var report = await _healthCheckService.CheckHealthAsync();
        var status = report.Status == HealthStatus.Healthy ? 200 : 503;
        return StatusCode(status, new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(e => new
            {
                name = e.Key,
                status = e.Value.Status.ToString(),
                duration = e.Value.Duration.TotalMilliseconds,
                description = e.Value.Description,
                exception = e.Value.Exception?.Message
            }),
            totalDuration = report.TotalDuration.TotalMilliseconds
        });
    }
}
