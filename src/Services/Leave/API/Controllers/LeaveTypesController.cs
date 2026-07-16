using HRMS.Services.Leave.Application.Commands.CreateLeaveType;
using HRMS.Services.Leave.Application.Commands.UpdateLeaveType;
using HRMS.Services.Leave.Application.Queries.GetLeaveTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Leave.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaveTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveTypesController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetLeaveTypesQuery query) =>
        Ok(await _mediator.Send(query));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLeaveTypeCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAll), new { }, id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLeaveTypeCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }
}
