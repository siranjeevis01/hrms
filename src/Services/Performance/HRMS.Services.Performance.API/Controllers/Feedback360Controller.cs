using HRMS.Services.Performance.Application.Commands.AddFeedbackAnswer;
using HRMS.Services.Performance.Application.Commands.CreateFeedback360;
using HRMS.Services.Performance.Application.Commands.SubmitFeedback360;
using HRMS.Services.Performance.Application.Queries.GetFeedback360;
using HRMS.Services.Performance.Application.Queries.GetFeedback360ByEmployee;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Performance.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Feedback360Controller : ControllerBase
{
    private readonly IMediator _mediator;

    public Feedback360Controller(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Feedback360Dto), 200)]
    public async Task<IActionResult> GetFeedback360(Guid id)
    {
        var result = await _mediator.Send(new GetFeedback360Query { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("employee/{employeeId:guid}")]
    [ProducesResponseType(typeof(List<Feedback360Dto>), 200)]
    public async Task<IActionResult> GetFeedback360ByEmployee(Guid employeeId, [FromQuery] string tenantId = "")
    {
        var result = await _mediator.Send(new GetFeedback360ByEmployeeQuery { EmployeeId = employeeId, TenantId = tenantId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateFeedback360([FromBody] CreateFeedback360Command command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetFeedback360), new { id }, id);
    }

    [HttpPost("{id:guid}/submit")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> SubmitFeedback360(Guid id)
    {
        await _mediator.Send(new SubmitFeedback360Command { FeedbackId = id });
        return NoContent();
    }

    [HttpPost("{id:guid}/answers")]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> AddFeedbackAnswer(Guid id, [FromBody] AddFeedbackAnswerCommand command)
    {
        command.Feedback360Id = id;
        var answerId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetFeedback360), new { id }, answerId);
    }
}
