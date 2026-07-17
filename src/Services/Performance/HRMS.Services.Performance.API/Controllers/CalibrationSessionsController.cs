using HRMS.Services.Performance.Application.Commands.AddCalibrationEntry;
using HRMS.Services.Performance.Application.Commands.CompleteCalibration;
using HRMS.Services.Performance.Application.Commands.CreateCalibrationSession;
using HRMS.Services.Performance.Application.Queries.GetCalibrationEntries;
using HRMS.Services.Performance.Application.Queries.GetCalibrationSession;
using HRMS.Services.Performance.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Performance.API.Controllers;

[ApiController]
[Route("api/performance/[controller]")]
public class CalibrationSessionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CalibrationSessionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CalibrationSessionDto), 200)]
    public async Task<IActionResult> GetCalibrationSession(Guid id)
    {
        var result = await _mediator.Send(new GetCalibrationSessionQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("{id:guid}/entries")]
    [ProducesResponseType(typeof(List<CalibrationEntryDto>), 200)]
    public async Task<IActionResult> GetCalibrationEntries(Guid id)
    {
        var result = await _mediator.Send(new GetCalibrationEntriesQuery { SessionId = id });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateCalibrationSession([FromBody] CreateCalibrationSessionCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetCalibrationSession), new { id }, id);
    }

    [HttpPost("{id:guid}/entries")]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> AddCalibrationEntry(Guid id, [FromBody] AddCalibrationEntryCommand command)
    {
        command.CalibrationSessionId = id;
        var entryId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetCalibrationSession), new { id }, entryId);
    }

    [HttpPost("{id:guid}/complete")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> CompleteCalibration(Guid id)
    {
        await _mediator.Send(new CompleteCalibrationCommand { SessionId = id });
        return NoContent();
    }
}
