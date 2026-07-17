using HRMS.Services.Attendance.Application.Commands.ApproveRegularization;
using HRMS.Services.Attendance.Application.Commands.RejectRegularization;
using HRMS.Services.Attendance.Application.Commands.RequestRegularization;
using HRMS.Services.Attendance.Application.Queries.GetPendingRegularizations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Attendance.API.Controllers;

[ApiController]
[Route("api/attendance/[controller]")]
public class RegularizationController : ControllerBase
{
    private readonly IMediator _mediator;

    public RegularizationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> RequestRegularization([FromBody] RequestRegularizationCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetPending), new { id }, new { id });
    }

    [HttpPut("{id:guid}/approve")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Approve(Guid id, [FromQuery] Guid approvedBy)
    {
        await _mediator.Send(new ApproveRegularizationCommand
        {
            RegularizationId = id,
            ApprovedBy = approvedBy
        });
        return Ok(new { message = "Regularization approved." });
    }

    [HttpPut("{id:guid}/reject")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Reject(Guid id, [FromBody] RejectRegularizationCommand command)
    {
        command.RegularizationId = id;
        await _mediator.Send(command);
        return Ok(new { message = "Regularization rejected." });
    }

    [HttpGet("pending")]
    [ProducesResponseType(typeof(List<Application.DTOs.RegularizationDto>), 200)]
    public async Task<IActionResult> GetPending([FromQuery] Guid? tenantId = null)
    {
        var result = await _mediator.Send(new GetPendingRegularizationsQuery { TenantId = tenantId });
        return Ok(result);
    }
}
