using HRMS.Services.Chat.Application.Commands.UpdatePresence;
using HRMS.Services.Chat.Application.DTOs;
using HRMS.Services.Chat.Application.Queries.GetPresence;
using HRMS.Services.Chat.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Chat.API.Controllers;

[ApiController]
[Route("api/chat/[controller]")]
public class PresenceController : ControllerBase
{
    private readonly IMediator _mediator;

    public PresenceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{employeeId:guid}")]
    [ProducesResponseType(typeof(UserPresenceDto), 200)]
    public async Task<IActionResult> GetPresence(Guid employeeId)
    {
        var result = await _mediator.Send(new GetPresenceQuery { EmployeeId = employeeId });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdatePresence([FromBody] UpdatePresenceCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}
