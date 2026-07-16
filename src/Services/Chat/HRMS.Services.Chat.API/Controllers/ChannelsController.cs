using HRMS.Services.Chat.Application.Commands.ArchiveChannel;
using HRMS.Services.Chat.Application.Commands.CreateChannel;
using HRMS.Services.Chat.Application.Commands.UpdateChannel;
using HRMS.Services.Chat.Application.DTOs;
using HRMS.Services.Chat.Application.Queries.GetChannelMessages;
using HRMS.Services.Chat.Application.Queries.GetChannels;
using HRMS.Shared.Kernel.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Chat.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChannelsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChannelsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ChatChannelDto>), 200)]
    public async Task<IActionResult> GetChannels(
        [FromQuery] Guid tenantId,
        [FromQuery] bool includeArchived = false)
    {
        var result = await _mediator.Send(new GetChannelsQuery
        {
            TenantId = tenantId,
            IncludeArchived = includeArchived
        });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateChannel([FromBody] CreateChannelCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetChannels), new { tenantId = command.TenantId }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateChannel(Guid id, [FromBody] UpdateChannelCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/archive")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ArchiveChannel(Guid id)
    {
        await _mediator.Send(new ArchiveChannelCommand { Id = id });
        return NoContent();
    }

    [HttpGet("{id:guid}/messages")]
    [ProducesResponseType(typeof(PagedResult<MessageDto>), 200)]
    public async Task<IActionResult> GetChannelMessages(
        Guid id,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 50)
    {
        var result = await _mediator.Send(new GetChannelMessagesQuery
        {
            ChannelId = id,
            PageNumber = pageNumber,
            PageSize = pageSize
        });
        return Ok(result);
    }
}
