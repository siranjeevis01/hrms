using HRMS.Services.Training.Application.Commands.AddAssessmentQuestion;
using HRMS.Services.Training.Application.Commands.CreateAssessment;
using HRMS.Services.Training.Application.Commands.SubmitAssessment;
using HRMS.Services.Training.Application.DTOs;
using HRMS.Services.Training.Application.Queries.GetAssessment;
using HRMS.Services.Training.Application.Queries.GetAssessmentAttempts;
using HRMS.Services.Training.Application.Queries.GetAssessmentQuestions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Training.API.Controllers;

[ApiController]
[Route("api/training/[controller]")]
public class AssessmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssessmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AssessmentDto), 200)]
    public async Task<IActionResult> GetAssessment(Guid id)
    {
        var result = await _mediator.Send(new GetAssessmentQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("{id:guid}/questions")]
    [ProducesResponseType(typeof(List<AssessmentQuestionDto>), 200)]
    public async Task<IActionResult> GetQuestions(Guid id)
    {
        var result = await _mediator.Send(new GetAssessmentQuestionsQuery { AssessmentId = id });
        return Ok(result);
    }

    [HttpGet("{id:guid}/attempts")]
    [ProducesResponseType(typeof(List<AssessmentAttemptDto>), 200)]
    public async Task<IActionResult> GetAttempts(Guid id, [FromQuery] Guid? employeeId = null)
    {
        var result = await _mediator.Send(new GetAssessmentAttemptsQuery { AssessmentId = id, EmployeeId = employeeId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateAssessment([FromBody] CreateAssessmentCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAssessment), new { id }, id);
    }

    [HttpPost("{id:guid}/questions")]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> AddQuestion(Guid id, [FromBody] AddAssessmentQuestionCommand command)
    {
        command.AssessmentId = id;
        var questionId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetQuestions), new { id }, questionId);
    }

    [HttpPost("{id:guid}/submit")]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> SubmitAssessment(Guid id, [FromBody] SubmitAssessmentCommand command)
    {
        command.AssessmentId = id;
        var attemptId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAttempts), new { id }, attemptId);
    }
}
