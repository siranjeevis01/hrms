using HRMS.Services.ProjectTask.Application.Commands.LogTime;
using HRMS.Services.ProjectTask.Application.Commands.UpdateTimeLog;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.ProjectTask.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimeLogsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TimeLogsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> LogTime([FromBody] LogTimeCommand command)
    {
        var id = await _mediator.Send(command);
        return Created(string.Empty, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateTimeLog(Guid id, [FromBody] UpdateTimeLogCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }
}
