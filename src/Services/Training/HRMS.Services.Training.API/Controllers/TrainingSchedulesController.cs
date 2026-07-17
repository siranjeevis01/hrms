using HRMS.Services.Training.Application.Commands.CreateTrainingSchedule;
using HRMS.Services.Training.Application.DTOs;
using HRMS.Services.Training.Application.Queries.GetTrainingSchedules;
using HRMS.Services.Training.Application.Queries.GetTrainingStats;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Training.API.Controllers;

[ApiController]
[Route("api/training/[controller]")]
public class TrainingSchedulesController : ControllerBase
{
    private readonly IMediator _mediator;

    public TrainingSchedulesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<TrainingScheduleDto>), 200)]
    public async Task<IActionResult> GetSchedules(
        [FromQuery] Guid? courseId = null,
        [FromQuery] string? status = null)
    {
        var result = await _mediator.Send(new GetTrainingSchedulesQuery { CourseId = courseId, Status = status });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateSchedule([FromBody] CreateTrainingScheduleCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(null, new { id }, id);
    }
}
