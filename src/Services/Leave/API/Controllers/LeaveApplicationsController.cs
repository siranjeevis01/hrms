using HRMS.Services.Leave.Application.Commands.ApplyLeave;
using HRMS.Services.Leave.Application.Commands.CancelLeave;
using HRMS.Services.Leave.Application.Queries.GetLeaveApplications;
using HRMS.Services.Leave.Application.Queries.GetMyLeaveApplications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Leave.API.Controllers;

[ApiController]
[Route("api/leave/[controller]")]
public class LeaveApplicationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveApplicationsController(IMediator mediator) => _mediator = mediator;

    [HttpGet("my")]
    public async Task<IActionResult> GetMyLeaves([FromQuery] GetMyLeaveApplicationsQuery query) =>
        Ok(await _mediator.Send(query));

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetLeaveApplicationsQuery query) =>
        Ok(await _mediator.Send(query));

    [HttpPost("apply")]
    public async Task<IActionResult> Apply([FromBody] ApplyLeaveCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetMyLeaves), new { }, id);
    }

    [HttpPost("{id}/cancel")]
    public async Task<IActionResult> Cancel(Guid id, [FromBody] CancelLeaveCommand command)
    {
        command.LeaveApplicationId = id;
        await _mediator.Send(command);
        return NoContent();
    }
}
