using HRMS.Services.Leave.Application.Commands.EarnCompOff;
using HRMS.Services.Leave.Application.Commands.UseCompOff;
using HRMS.Services.Leave.Application.Queries.GetCompOffBalances;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Leave.API.Controllers;

[ApiController]
[Route("api/leave/[controller]")]
public class CompOffController : ControllerBase
{
    private readonly IMediator _mediator;

    public CompOffController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetBalances([FromQuery] GetCompOffBalancesQuery query) =>
        Ok(await _mediator.Send(query));

    [HttpPost("earn")]
    public async Task<IActionResult> Earn([FromBody] EarnCompOffCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetBalances), new { }, id);
    }

    [HttpPost("{id}/use")]
    public async Task<IActionResult> Use(Guid id, [FromBody] UseCompOffCommand command)
    {
        command.CompOffId = id;
        await _mediator.Send(command);
        return NoContent();
    }
}
