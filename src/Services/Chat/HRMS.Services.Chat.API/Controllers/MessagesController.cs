using HRMS.Services.Chat.Application.Commands.AddReaction;
using HRMS.Services.Chat.Application.Commands.DeleteMessage;
using HRMS.Services.Chat.Application.Commands.EditMessage;
using HRMS.Services.Chat.Application.Commands.MarkAsRead;
using HRMS.Services.Chat.Application.Commands.RemoveReaction;
using HRMS.Services.Chat.Application.Commands.SendMessage;
using HRMS.Services.Chat.Application.DTOs;
using HRMS.Services.Chat.Application.Queries.GetMessage;
using HRMS.Services.Chat.Application.Queries.GetMessageReactions;
using HRMS.Services.Chat.Application.Queries.GetMessages;
using HRMS.Services.Chat.Application.Queries.SearchMessages;
using HRMS.Shared.Kernel.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Chat.API.Controllers;

[ApiController]
[Route("api/chat/[controller]")]
public class MessagesController : ControllerBase
{
    private readonly IMediator _mediator;

    public MessagesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("conversation/{conversationId:guid}")]
    [ProducesResponseType(typeof(PagedResult<MessageDto>), 200)]
    public async Task<IActionResult> GetMessages(
        Guid conversationId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 50)
    {
        var query = new GetMessagesQuery
        {
            ConversationId = conversationId,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(MessageDto), 200)]
    public async Task<IActionResult> GetMessage(Guid id)
    {
        var result = await _mediator.Send(new GetMessageQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetMessage), new { id }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> EditMessage(Guid id, [FromBody] EditMessageCommand command)
    {
        command.MessageId = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteMessage(Guid id)
    {
        await _mediator.Send(new DeleteMessageCommand { MessageId = id });
        return NoContent();
    }

    [HttpPost("{id:guid}/reactions")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> AddReaction(Guid id, [FromBody] AddReactionCommand command)
    {
        command.MessageId = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}/reactions")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> RemoveReaction(Guid id, [FromQuery] Guid employeeId, [FromQuery] string emoji)
    {
        await _mediator.Send(new RemoveReactionCommand
        {
            MessageId = id,
            EmployeeId = employeeId,
            Emoji = emoji
        });
        return NoContent();
    }

    [HttpGet("{id:guid}/reactions")]
    [ProducesResponseType(typeof(List<MessageReactionDto>), 200)]
    public async Task<IActionResult> GetReactions(Guid id)
    {
        var result = await _mediator.Send(new GetMessageReactionsQuery { MessageId = id });
        return Ok(result);
    }

    [HttpPost("{id:guid}/read")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> MarkAsRead(Guid id, [FromQuery] Guid conversationId, [FromQuery] Guid employeeId)
    {
        await _mediator.Send(new MarkAsReadCommand
        {
            ConversationId = conversationId,
            EmployeeId = employeeId,
            MessageId = id
        });
        return NoContent();
    }

    [HttpGet("search")]
    [ProducesResponseType(typeof(List<MessageDto>), 200)]
    public async Task<IActionResult> SearchMessages(
        [FromQuery] Guid conversationId,
        [FromQuery] string searchTerm,
        [FromQuery] int maxResults = 50)
    {
        var result = await _mediator.Send(new SearchMessagesQuery
        {
            ConversationId = conversationId,
            SearchTerm = searchTerm,
            MaxResults = maxResults
        });
        return Ok(result);
    }
}
