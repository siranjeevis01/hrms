using HRMS.Services.Dashboard.Application.Commands.TrackAnalyticsEvent;
using HRMS.Services.Dashboard.Application.DTOs;
using HRMS.Services.Dashboard.Application.Queries.GetAnalytics;
using HRMS.Services.Dashboard.Application.Queries.GetEmployeeAnalytics;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Dashboard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AnalyticsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<AnalyticsEventDto>), 200)]
    public async Task<IActionResult> GetAnalytics(
        [FromQuery] string? entityType = null,
        [FromQuery] DateTime? fromDate = null,
        [FromQuery] DateTime? toDate = null,
        [FromQuery] int maxResults = 100,
        [FromQuery] string tenantId = "")
    {
        var query = new GetAnalyticsQuery
        {
            EntityType = entityType,
            FromDate = fromDate,
            ToDate = toDate,
            MaxResults = maxResults,
            TenantId = tenantId
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("employee/{employeeId:guid}")]
    [ProducesResponseType(typeof(EmployeeAnalyticsDto), 200)]
    public async Task<IActionResult> GetEmployeeAnalytics(
        Guid employeeId,
        [FromQuery] DateTime? fromDate = null,
        [FromQuery] DateTime? toDate = null,
        [FromQuery] string tenantId = "")
    {
        var result = await _mediator.Send(new GetEmployeeAnalyticsQuery
        {
            EmployeeId = employeeId,
            FromDate = fromDate,
            ToDate = toDate,
            TenantId = tenantId
        });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> TrackEvent([FromBody] TrackAnalyticsEventCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAnalytics), new { tenantId = command.TenantId }, id);
    }
}
