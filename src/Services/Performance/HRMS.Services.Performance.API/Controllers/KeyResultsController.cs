using HRMS.Services.Performance.Application.Commands.CreateKeyResult;
using HRMS.Services.Performance.Application.Commands.UpdateKeyResult;
using HRMS.Services.Performance.Application.Queries.GetKeyResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Performance.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KeyResultsController : ControllerBase
{
    private readonly IMediator _mediator;

    public KeyResultsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("goal/{goalId:guid}")]
    [ProducesResponseType(typeof(List<KeyResultDto>), 200)]
    public async Task<IActionResult> GetKeyResults(Guid goalId)
    {
        var result = await _mediator.Send(new GetKeyResultsQuery { GoalId = goalId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateKeyResult([FromBody] CreateKeyResultCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetKeyResults), new { goalId = command.GoalId }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateKeyResult(Guid id, [FromBody] UpdateKeyResultCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }
}
