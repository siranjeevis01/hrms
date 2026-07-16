using HRMS.Services.Dashboard.Application.Commands.AddWidget;
using HRMS.Services.Dashboard.Application.Commands.RemoveWidget;
using HRMS.Services.Dashboard.Application.Commands.UpdateWidget;
using HRMS.Services.Dashboard.Application.DTOs;
using HRMS.Services.Dashboard.Application.Queries.GetDashboardWidgets;
using HRMS.Services.Dashboard.Application.Queries.GetWidgetPresets;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Dashboard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WidgetsController : ControllerBase
{
    private readonly IMediator _mediator;

    public WidgetsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("dashboard/{dashboardId:guid}")]
    [ProducesResponseType(typeof(List<DashboardWidgetDto>), 200)]
    public async Task<IActionResult> GetDashboardWidgets(Guid dashboardId, [FromQuery] string tenantId = "")
    {
        var result = await _mediator.Send(new GetDashboardWidgetsQuery { DashboardId = dashboardId, TenantId = tenantId });
        return Ok(result);
    }

    [HttpGet("presets")]
    [ProducesResponseType(typeof(List<WidgetPresetDto>), 200)]
    public async Task<IActionResult> GetWidgetPresets(
        [FromQuery] string? category = null,
        [FromQuery] string tenantId = "")
    {
        var result = await _mediator.Send(new GetWidgetPresetsQuery { Category = category, TenantId = tenantId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> AddWidget([FromBody] AddWidgetCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetDashboardWidgets), new { dashboardId = command.DashboardId, tenantId = command.TenantId }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateWidget(Guid id, [FromBody] UpdateWidgetCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> RemoveWidget(Guid id, [FromQuery] Guid dashboardId, [FromQuery] string tenantId = "")
    {
        await _mediator.Send(new RemoveWidgetCommand { Id = id, DashboardId = dashboardId, TenantId = tenantId });
        return NoContent();
    }
}
