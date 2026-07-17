using HRMS.Services.Performance.Application.Commands.AddOKRItem;
using HRMS.Services.Performance.Application.Commands.ApproveOKR;
using HRMS.Services.Performance.Application.Commands.CreateOKR;
using HRMS.Services.Performance.Application.Commands.RejectOKR;
using HRMS.Services.Performance.Application.Commands.SubmitOKR;
using HRMS.Services.Performance.Application.Commands.UpdateOKR;
using HRMS.Services.Performance.Application.Queries.GetEmployeeOKRs;
using HRMS.Services.Performance.Application.Queries.GetOKR;
using HRMS.Services.Performance.Application.Queries.GetOKRs;
using HRMS.Services.Performance.Application.DTOs;
using HRMS.Services.Performance.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Performance.API.Controllers;

[ApiController]
[Route("api/performance/[controller]")]
public class OKRsController : ControllerBase
{
    private readonly IMediator _mediator;

    public OKRsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<OKRDto>), 200)]
    public async Task<IActionResult> GetOKRs(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] OKRStatus? status = null,
        [FromQuery] string? period = null,
        [FromQuery] string tenantId = "")
    {
        var query = new GetOKRsQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            Status = status,
            Period = period,
            TenantId = tenantId
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(OKRDto), 200)]
    public async Task<IActionResult> GetOKR(Guid id)
    {
        var result = await _mediator.Send(new GetOKRQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("employee/{employeeId:guid}")]
    [ProducesResponseType(typeof(List<OKRDto>), 200)]
    public async Task<IActionResult> GetEmployeeOKRs(Guid employeeId, [FromQuery] string tenantId = "")
    {
        var result = await _mediator.Send(new GetEmployeeOKRsQuery { EmployeeId = employeeId, TenantId = tenantId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateOKR([FromBody] CreateOKRCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetOKR), new { id }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateOKR(Guid id, [FromBody] UpdateOKRCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/submit")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> SubmitOKR(Guid id)
    {
        await _mediator.Send(new SubmitOKRCommand { OKRId = id });
        return NoContent();
    }

    [HttpPost("{id:guid}/approve")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ApproveOKR(Guid id, [FromBody] ApproveOKRCommand command)
    {
        command.OKRId = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/reject")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> RejectOKR(Guid id)
    {
        await _mediator.Send(new RejectOKRCommand { OKRId = id });
        return NoContent();
    }

    [HttpPost("{id:guid}/items")]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> AddOKRItem(Guid id, [FromBody] AddOKRItemCommand command)
    {
        command.OKRId = id;
        var itemId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetOKR), new { id }, itemId);
    }
}
