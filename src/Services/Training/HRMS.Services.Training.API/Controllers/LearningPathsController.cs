using HRMS.Services.Training.Application.Commands.AddCourseToLearningPath;
using HRMS.Services.Training.Application.Commands.CreateLearningPath;
using HRMS.Services.Training.Application.DTOs;
using HRMS.Services.Training.Application.Queries.GetLearningPath;
using HRMS.Services.Training.Application.Queries.GetLearningPaths;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Training.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LearningPathsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LearningPathsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<LearningPathDto>), 200)]
    public async Task<IActionResult> GetLearningPaths([FromQuery] Guid? departmentId = null)
    {
        var result = await _mediator.Send(new GetLearningPathsQuery { DepartmentId = departmentId });
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(LearningPathDto), 200)]
    public async Task<IActionResult> GetLearningPath(Guid id)
    {
        var result = await _mediator.Send(new GetLearningPathQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateLearningPath([FromBody] CreateLearningPathCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetLearningPath), new { id }, id);
    }

    [HttpPost("{id:guid}/courses")]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> AddCourse(Guid id, [FromBody] AddCourseToLearningPathCommand command)
    {
        command.LearningPathId = id;
        var courseId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetLearningPath), new { id }, courseId);
    }
}
