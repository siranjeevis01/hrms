using HRMS.Services.ProjectTask.Application.Commands.CreateEpic;
using HRMS.Services.ProjectTask.Application.Commands.UpdateEpic;
using HRMS.Services.ProjectTask.Application.DTOs;
using HRMS.Services.ProjectTask.Application.Queries.GetProjectEpics;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.ProjectTask.API.Controllers;

[ApiController]
[Route("api/projects/{projectId:guid}/epics")]
public class EpicsController : ControllerBase
{
    private readonly IMediator _mediator;

    public EpicsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<EpicDto>), 200)]
    public async Task<IActionResult> GetEpics(Guid projectId)
    {
        var result = await _mediator.Send(new GetProjectEpicsQuery { ProjectId = projectId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateEpic(Guid projectId, [FromBody] CreateEpicCommand command)
    {
        command.ProjectId = projectId;
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetEpics), new { projectId }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateEpic(Guid projectId, Guid id, [FromBody] UpdateEpicCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }
}
