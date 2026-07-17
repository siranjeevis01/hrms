using HRMS.Services.Performance.Application.Commands.CreateKPI;
using HRMS.Services.Performance.Application.Commands.UpdateKPI;
using HRMS.Services.Performance.Application.Commands.UpdateKPIValue;
using HRMS.Services.Performance.Application.Queries.GetEmployeeKPIs;
using HRMS.Services.Performance.Application.Queries.GetKPI;
using HRMS.Services.Performance.Application.Queries.GetKPIs;
using HRMS.Services.Performance.Application.DTOs;
using HRMS.Services.Performance.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Performance.API.Controllers;

[ApiController]
[Route("api/performance/[controller]")]
public class KPIsController : ControllerBase
{
    private readonly IMediator _mediator;

    public KPIsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<KPIDto>), 200)]
    public async Task<IActionResult> GetKPIs(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] KPICategory? category = null,
        [FromQuery] string? period = null,
        [FromQuery] Guid? departmentId = null,
        [FromQuery] string tenantId = "")
    {
        var query = new GetKPIsQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            Category = category,
            Period = period,
            DepartmentId = departmentId,
            TenantId = tenantId
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(KPIDto), 200)]
    public async Task<IActionResult> GetKPI(Guid id)
    {
        var result = await _mediator.Send(new GetKPIQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("employee/{employeeId:guid}")]
    [ProducesResponseType(typeof(List<KPIDto>), 200)]
    public async Task<IActionResult> GetEmployeeKPIs(Guid employeeId, [FromQuery] string tenantId = "")
    {
        var result = await _mediator.Send(new GetEmployeeKPIsQuery { EmployeeId = employeeId, TenantId = tenantId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateKPI([FromBody] CreateKPICommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetKPI), new { id }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateKPI(Guid id, [FromBody] UpdateKPICommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id:guid}/value")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateKPIValue(Guid id, [FromBody] UpdateKPIValueCommand command)
    {
        command.KPIId = id;
        await _mediator.Send(command);
        return NoContent();
    }
}
