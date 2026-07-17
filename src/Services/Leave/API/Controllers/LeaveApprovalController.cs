using HRMS.Services.Leave.Application.Commands.ApproveLeave;
using HRMS.Services.Leave.Application.Commands.RejectLeave;
using HRMS.Services.Leave.Application.Queries.GetPendingApprovals;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Leave.API.Controllers;

[ApiController]
[Route("api/leave/[controller]")]
public class LeaveApprovalController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveApprovalController(IMediator mediator) => _mediator = mediator;

    [HttpGet("pending")]
    public async Task<IActionResult> GetPending([FromQuery] GetPendingApprovalsQuery query) =>
        Ok(await _mediator.Send(query));

    [HttpPost("{id}/approve")]
    public async Task<IActionResult> Approve(Guid id, [FromBody] ApproveLeaveCommand command)
    {
        command.LeaveApplicationId = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id}/reject")]
    public async Task<IActionResult> Reject(Guid id, [FromBody] RejectLeaveCommand command)
    {
        command.LeaveApplicationId = id;
        await _mediator.Send(command);
        return NoContent();
    }
}
