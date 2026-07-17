using HRMS.Services.Training.Application.Commands.EnrollEmployee;
using HRMS.Services.Training.Application.Commands.UnenrollEmployee;
using HRMS.Services.Training.Application.Commands.UpdateLessonProgress;
using HRMS.Services.Training.Application.DTOs;
using HRMS.Services.Training.Application.Queries.GetCourseEnrollments;
using HRMS.Services.Training.Application.Queries.GetEmployeeEnrollments;
using HRMS.Services.Training.Application.Queries.GetEnrollment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Training.API.Controllers;

[ApiController]
[Route("api/training/[controller]")]
public class EnrollmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public EnrollmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(EnrollmentDto), 200)]
    public async Task<IActionResult> GetEnrollment(Guid id)
    {
        var result = await _mediator.Send(new GetEnrollmentQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("by-employee/{employeeId:guid}")]
    [ProducesResponseType(typeof(List<EnrollmentDto>), 200)]
    public async Task<IActionResult> GetEmployeeEnrollments(Guid employeeId)
    {
        var result = await _mediator.Send(new GetEmployeeEnrollmentsQuery { EmployeeId = employeeId });
        return Ok(result);
    }

    [HttpGet("by-course/{courseId:guid}")]
    [ProducesResponseType(typeof(List<EnrollmentDto>), 200)]
    public async Task<IActionResult> GetCourseEnrollments(Guid courseId)
    {
        var result = await _mediator.Send(new GetCourseEnrollmentsQuery { CourseId = courseId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> EnrollEmployee([FromBody] EnrollEmployeeCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetEnrollment), new { id }, id);
    }

    [HttpPost("{id:guid}/drop")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DropEnrollment(Guid id)
    {
        await _mediator.Send(new UnenrollEmployeeCommand { EnrollmentId = id });
        return NoContent();
    }

    [HttpPost("{enrollmentId:guid}/lessons/{lessonId:guid}/progress")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateLessonProgress(Guid enrollmentId, Guid lessonId, [FromBody] UpdateLessonProgressCommand command)
    {
        command.EnrollmentId = enrollmentId;
        command.LessonId = lessonId;
        await _mediator.Send(command);
        return NoContent();
    }
}
