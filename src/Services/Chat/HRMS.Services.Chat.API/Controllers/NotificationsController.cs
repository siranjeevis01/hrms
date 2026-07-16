using HRMS.Services.Chat.Application.Commands.CreateNotification;
using HRMS.Services.Chat.Application.Commands.MarkNotificationRead;
using HRMS.Services.Chat.Application.DTOs;
using HRMS.Services.Chat.Application.Queries.GetConversationSummaries;
using HRMS.Services.Chat.Application.Queries.GetNotifications;
using HRMS.Shared.Kernel.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Chat.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<ChatNotificationDto>), 200)]
    public async Task<IActionResult> GetNotifications(
        [FromQuery] Guid employeeId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] bool unreadOnly = false)
    {
        var query = new GetNotificationsQuery
        {
            EmployeeId = employeeId,
            PageNumber = pageNumber,
            PageSize = pageSize,
            UnreadOnly = unreadOnly
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetNotifications), new { employeeId = command.EmployeeId }, id);
    }

    [HttpPut("{id:guid}/read")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> MarkNotificationRead(Guid id)
    {
        await _mediator.Send(new MarkNotificationReadCommand { NotificationId = id });
        return NoContent();
    }

    [HttpGet("summaries/{employeeId:guid}")]
    [ProducesResponseType(typeof(List<ConversationSummaryDto>), 200)]
    public async Task<IActionResult> GetConversationSummaries(Guid employeeId)
    {
        var result = await _mediator.Send(new GetConversationSummariesQuery { EmployeeId = employeeId });
        return Ok(result);
    }
}
