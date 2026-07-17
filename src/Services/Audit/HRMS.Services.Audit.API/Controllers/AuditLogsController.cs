using HRMS.Services.Audit.Application.Commands.LogAuditEntry;
using HRMS.Services.Audit.Application.Commands.LogDataChange;
using HRMS.Services.Audit.Application.Commands.LogLogin;
using HRMS.Services.Audit.Application.DTOs;
using HRMS.Services.Audit.Application.Queries.GetAuditLogs;
using HRMS.Services.Audit.Application.Queries.GetAuditStats;
using HRMS.Services.Audit.Application.Queries.GetAuditTrail;
using HRMS.Services.Audit.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Audit.API.Controllers;

[ApiController]
[Route("api/audit/[controller]")]
public class AuditLogsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuditLogsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedAuditResult<AuditLogDto>), 200)]
    public async Task<IActionResult> GetAuditLogs(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] DateTime? fromDate = null,
        [FromQuery] DateTime? toDate = null,
        [FromQuery] AuditEntityType? entityType = null,
        [FromQuery] AuditActionType? actionType = null,
        [FromQuery] Guid? userId = null,
        [FromQuery] string? searchTerm = null,
        [FromQuery] string tenantId = "")
    {
        var query = new GetAuditLogsQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            FromDate = fromDate,
            ToDate = toDate,
            EntityType = entityType,
            ActionType = actionType,
            UserId = userId,
            SearchTerm = searchTerm,
            TenantId = tenantId
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}/trail")]
    [ProducesResponseType(typeof(List<AuditTrailDto>), 200)]
    public async Task<IActionResult> GetAuditTrail(Guid id, [FromQuery] string tenantId = "")
    {
        var result = await _mediator.Send(new GetAuditTrailQuery { AuditLogId = id, TenantId = tenantId });
        return Ok(result);
    }

    [HttpGet("stats")]
    [ProducesResponseType(typeof(AuditStatsDto), 200)]
    public async Task<IActionResult> GetAuditStats(
        [FromQuery] DateTime? fromDate = null,
        [FromQuery] DateTime? toDate = null,
        [FromQuery] string tenantId = "")
    {
        var result = await _mediator.Send(new GetAuditStatsQuery
        {
            FromDate = fromDate,
            ToDate = toDate,
            TenantId = tenantId
        });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> LogAuditEntry([FromBody] LogAuditEntryCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAuditTrail), new { id, tenantId = command.TenantId }, id);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> LogLogin([FromBody] LogLoginCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAuditLogs), new { tenantId = command.TenantId }, id);
    }

    [HttpPost("data-change")]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> LogDataChange([FromBody] LogDataChangeCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAuditLogs), new { tenantId = command.TenantId }, id);
    }
}
