using HRMS.Services.Training.Application.Commands.CreateCourse;
using HRMS.Services.Training.Application.Commands.PublishCourse;
using HRMS.Services.Training.Application.Commands.UnpublishCourse;
using HRMS.Services.Training.Application.Commands.UpdateCourse;
using HRMS.Services.Training.Application.DTOs;
using HRMS.Services.Training.Application.Queries.GetCourse;
using HRMS.Services.Training.Application.Queries.GetCourseAnalytics;
using HRMS.Services.Training.Application.Queries.GetCourses;
using HRMS.Shared.Kernel.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Training.API.Controllers;

[ApiController]
[Route("api/training/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CoursesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<CourseDto>), 200)]
    public async Task<IActionResult> GetCourses(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? searchTerm = null,
        [FromQuery] string? category = null,
        [FromQuery] string? status = null,
        [FromQuery] Guid? departmentId = null)
    {
        var query = new GetCoursesQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            SearchTerm = searchTerm,
            Category = category,
            Status = status,
            DepartmentId = departmentId
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CourseDto), 200)]
    public async Task<IActionResult> GetCourse(Guid id)
    {
        var result = await _mediator.Send(new GetCourseQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetCourse), new { id }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateCourse(Guid id, [FromBody] UpdateCourseCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/publish")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> PublishCourse(Guid id)
    {
        await _mediator.Send(new PublishCourseCommand { Id = id });
        return NoContent();
    }

    [HttpPost("{id:guid}/unpublish")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UnpublishCourse(Guid id)
    {
        await _mediator.Send(new UnpublishCourseCommand { Id = id });
        return NoContent();
    }

    [HttpGet("{id:guid}/analytics")]
    [ProducesResponseType(typeof(CourseAnalyticsDto), 200)]
    public async Task<IActionResult> GetCourseAnalytics(Guid id)
    {
        var result = await _mediator.Send(new GetCourseAnalyticsQuery { CourseId = id });
        if (result == null) return NotFound();
        return Ok(result);
    }
}
