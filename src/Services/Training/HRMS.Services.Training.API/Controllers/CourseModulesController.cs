using HRMS.Services.Training.Application.Commands.AddModule;
using HRMS.Services.Training.Application.Commands.UpdateModule;
using HRMS.Services.Training.Application.DTOs;
using HRMS.Services.Training.Application.Queries.GetCourseModules;
using HRMS.Services.Training.Application.Queries.GetModuleLessons;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Training.API.Controllers;

[ApiController]
[Route("api/courses/{courseId:guid}/modules")]
public class CourseModulesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CourseModulesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<CourseModuleDto>), 200)]
    public async Task<IActionResult> GetModules(Guid courseId)
    {
        var result = await _mediator.Send(new GetCourseModulesQuery { CourseId = courseId });
        return Ok(result);
    }

    [HttpGet("{id:guid}/lessons")]
    [ProducesResponseType(typeof(List<LessonDto>), 200)]
    public async Task<IActionResult> GetModuleLessons(Guid courseId, Guid id)
    {
        var result = await _mediator.Send(new GetModuleLessonsQuery { ModuleId = id });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> AddModule(Guid courseId, [FromBody] AddModuleCommand command)
    {
        command.CourseId = courseId;
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetModules), new { courseId }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateModule(Guid courseId, Guid id, [FromBody] UpdateModuleCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }
}
