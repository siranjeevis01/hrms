using HRMS.Services.Chat.Application.Commands.AddParticipant;
using HRMS.Services.Chat.Application.Commands.CreateConversation;
using HRMS.Services.Chat.Application.Commands.RemoveParticipant;
using HRMS.Services.Chat.Application.Commands.UpdateConversation;
using HRMS.Services.Chat.Application.DTOs;
using HRMS.Services.Chat.Application.Queries.GetConversation;
using HRMS.Services.Chat.Application.Queries.GetConversationParticipants;
using HRMS.Services.Chat.Application.Queries.GetConversations;
using HRMS.Shared.Kernel.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Chat.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConversationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConversationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<ConversationDto>), 200)]
    public async Task<IActionResult> GetConversations(
        [FromQuery] Guid employeeId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetConversationsQuery
        {
            EmployeeId = employeeId,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ConversationDto), 200)]
    public async Task<IActionResult> GetConversation(Guid id)
    {
        var result = await _mediator.Send(new GetConversationQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateConversation([FromBody] CreateConversationCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetConversation), new { id }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateConversation(Guid id, [FromBody] UpdateConversationCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/participants")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> AddParticipant(Guid id, [FromBody] AddParticipantCommand command)
    {
        command.ConversationId = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}/participants/{employeeId:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> RemoveParticipant(Guid id, Guid employeeId)
    {
        await _mediator.Send(new RemoveParticipantCommand
        {
            ConversationId = id,
            EmployeeId = employeeId
        });
        return NoContent();
    }

    [HttpGet("{id:guid}/participants")]
    [ProducesResponseType(typeof(List<ConversationParticipantDto>), 200)]
    public async Task<IActionResult> GetParticipants(Guid id)
    {
        var result = await _mediator.Send(new GetConversationParticipantsQuery { ConversationId = id });
        return Ok(result);
    }
}
