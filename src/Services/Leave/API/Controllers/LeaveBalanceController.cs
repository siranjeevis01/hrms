using HRMS.Services.Leave.Application.Commands.AdjustLeaveBalance;
using HRMS.Services.Leave.Application.Commands.AllocateLeaveBalance;
using HRMS.Services.Leave.Application.Queries.GetMyLeaveBalance;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Leave.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaveBalanceController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveBalanceController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetBalance([FromQuery] GetMyLeaveBalanceQuery query) =>
        Ok(await _mediator.Send(query));

    [HttpPost("allocate")]
    public async Task<IActionResult> Allocate([FromBody] AllocateLeaveBalanceCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPost("adjust")]
    public async Task<IActionResult> Adjust([FromBody] AdjustLeaveBalanceCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}
