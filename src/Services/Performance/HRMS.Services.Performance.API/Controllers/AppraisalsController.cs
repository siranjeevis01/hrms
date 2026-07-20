using HRMS.Services.Performance.Application.Commands.ApproveAppraisal;
using HRMS.Services.Performance.Application.Commands.CreateAppraisal;
using HRMS.Services.Performance.Application.Commands.RejectAppraisal;
using HRMS.Services.Performance.Application.Commands.SubmitAppraisal;
using HRMS.Services.Performance.Application.Commands.SubmitSelfAssessment;
using HRMS.Services.Performance.Application.Queries.GetAppraisal;
using HRMS.Services.Performance.Application.Queries.GetAppraisals;
using HRMS.Services.Performance.Application.Queries.GetEmployeeAppraisals;
using HRMS.Services.Performance.Application.DTOs;
using HRMS.Services.Performance.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Performance.API.Controllers;

[ApiController]
[Route("api/performance/[controller]")]
public class AppraisalsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AppraisalsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<AppraisalDto>), 200)]
    public async Task<IActionResult> GetAppraisals(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] AppraisalStatus? status = null,
        [FromQuery] AppraisalType? type = null,
        [FromQuery] string? period = null,
        [FromQuery] string tenantId = "")
    {
        var query = new GetAppraisalsQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            Status = status,
            Type = type,
            Period = period,
            TenantId = tenantId
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AppraisalDto), 200)]
    public async Task<IActionResult> GetAppraisal(Guid id)
    {
        var result = await _mediator.Send(new GetAppraisalQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("employee/{employeeId:guid}")]
    [ProducesResponseType(typeof(List<AppraisalDto>), 200)]
    public async Task<IActionResult> GetEmployeeAppraisals(Guid employeeId, [FromQuery] string tenantId = "")
    {
        var result = await _mediator.Send(new GetEmployeeAppraisalsQuery { EmployeeId = employeeId, TenantId = tenantId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateAppraisal([FromBody] CreateAppraisalCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAppraisal), new { id }, id);
    }

    [HttpPost("{id:guid}/submit")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> SubmitAppraisal(Guid id)
    {
        await _mediator.Send(new SubmitAppraisalCommand { AppraisalId = id });
        return NoContent();
    }

    [HttpPost("{id:guid}/self-assessment")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> SubmitSelfAssessment(Guid id, [FromBody] SubmitSelfAssessmentCommand command)
    {
        command.AppraisalId = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/approve")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ApproveAppraisal(Guid id, [FromBody] ApproveAppraisalCommand command)
    {
        command.AppraisalId = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/reject")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> RejectAppraisal(Guid id)
    {
        await _mediator.Send(new RejectAppraisalCommand { AppraisalId = id });
        return NoContent();
    }
}
