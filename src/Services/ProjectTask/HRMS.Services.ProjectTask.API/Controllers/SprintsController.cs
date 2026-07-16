using HRMS.Services.ProjectTask.Application.Commands.CompleteSprint;
using HRMS.Services.ProjectTask.Application.Commands.CreateSprint;
using HRMS.Services.ProjectTask.Application.Commands.StartSprint;
using HRMS.Services.ProjectTask.Application.DTOs;
using HRMS.Services.ProjectTask.Application.Queries.GetSprint;
using HRMS.Services.ProjectTask.Application.Queries.GetSprints;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.ProjectTask.API.Controllers;

[ApiController]
[Route("api/projects/{projectId:guid}/sprints")]
public class SprintsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SprintsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<SprintDto>), 200)]
    public async Task<IActionResult> GetSprints(Guid projectId)
    {
        var result = await _mediator.Send(new GetSprintsQuery { ProjectId = projectId });
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SprintDto), 200)]
    public async Task<IActionResult> GetSprint(Guid projectId, Guid id)
    {
        var result = await _mediator.Send(new GetSprintQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateSprint(Guid projectId, [FromBody] CreateSprintCommand command)
    {
        command.ProjectId = projectId;
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetSprint), new { projectId, id }, id);
    }

    [HttpPost("{id:guid}/start")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> StartSprint(Guid projectId, Guid id)
    {
        await _mediator.Send(new StartSprintCommand { Id = id });
        return NoContent();
    }

    [HttpPost("{id:guid}/complete")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> CompleteSprint(Guid projectId, Guid id)
    {
        await _mediator.Send(new CompleteSprintCommand { Id = id });
        return NoContent();
    }
}
