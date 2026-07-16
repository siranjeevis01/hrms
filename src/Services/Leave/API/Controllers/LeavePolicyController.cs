using HRMS.Services.Leave.Application.Commands.UpdateLeavePolicy;
using HRMS.Services.Leave.Application.Queries.GetLeavePolicy;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Leave.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeavePolicyController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeavePolicyController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetLeavePolicyQuery query) =>
        Ok(await _mediator.Send(query));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLeavePolicyCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }
}
