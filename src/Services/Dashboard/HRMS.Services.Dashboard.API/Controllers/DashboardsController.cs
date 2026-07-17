using HRMS.Services.Dashboard.Application.Commands.CreateDashboard;
using HRMS.Services.Dashboard.Application.Commands.ShareDashboard;
using HRMS.Services.Dashboard.Application.Commands.UpdateDashboard;
using HRMS.Services.Dashboard.Application.DTOs;
using HRMS.Services.Dashboard.Application.Queries.GetDashboard;
using HRMS.Services.Dashboard.Application.Queries.GetDashboardShares;
using HRMS.Services.Dashboard.Application.Queries.GetDashboards;
using HRMS.Services.Dashboard.Application.Queries.GetDashboardStats;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Dashboard.API.Controllers;

[ApiController]
[Route("api/dashboard/[controller]")]
public class DashboardsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DashboardsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedDashboardResult<DashboardDto>), 200)]
    public async Task<IActionResult> GetDashboards(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Guid? userId = null,
        [FromQuery] string? searchTerm = null,
        [FromQuery] string tenantId = "")
    {
        var query = new GetDashboardsQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            UserId = userId,
            SearchTerm = searchTerm,
            TenantId = tenantId
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(DashboardDto), 200)]
    public async Task<IActionResult> GetDashboard(Guid id, [FromQuery] string tenantId = "")
    {
        var result = await _mediator.Send(new GetDashboardQuery { Id = id, TenantId = tenantId });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("stats")]
    [ProducesResponseType(typeof(DashboardStatsDto), 200)]
    public async Task<IActionResult> GetDashboardStats([FromQuery] string tenantId = "")
    {
        var result = await _mediator.Send(new GetDashboardStatsQuery { TenantId = tenantId });
        return Ok(result);
    }

    [HttpGet("{id:guid}/shares")]
    [ProducesResponseType(typeof(List<DashboardShareDto>), 200)]
    public async Task<IActionResult> GetDashboardShares(Guid id, [FromQuery] string tenantId = "")
    {
        var result = await _mediator.Send(new GetDashboardSharesQuery { DashboardId = id, TenantId = tenantId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateDashboard([FromBody] CreateDashboardCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetDashboard), new { id, tenantId = command.TenantId }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateDashboard(Guid id, [FromBody] UpdateDashboardCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/share")]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> ShareDashboard(Guid id, [FromBody] ShareDashboardCommand command)
    {
        command.DashboardId = id;
        var shareId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetDashboardShares), new { id, tenantId = command.TenantId }, shareId);
    }
}
