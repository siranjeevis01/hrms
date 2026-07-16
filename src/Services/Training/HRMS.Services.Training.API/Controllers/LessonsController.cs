using HRMS.Services.Training.Application.Commands.AddLesson;
using HRMS.Services.Training.Application.Commands.UpdateLesson;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Training.API.Controllers;

[ApiController]
[Route("api/modules/{moduleId:guid}/lessons")]
public class LessonsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LessonsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> AddLesson(Guid moduleId, [FromBody] AddLessonCommand command)
    {
        command.ModuleId = moduleId;
        var id = await _mediator.Send(command);
        return CreatedAtAction(null, new { moduleId }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateLesson(Guid moduleId, Guid id, [FromBody] UpdateLessonCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }
}
