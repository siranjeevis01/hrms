using HRMS.Services.Notification.Application.Commands.DeleteNotification;
using HRMS.Services.Notification.Application.Commands.MarkAllAsRead;
using HRMS.Services.Notification.Application.Commands.MarkAsRead;
using HRMS.Services.Notification.Application.Queries.GetMyNotifications;
using HRMS.Services.Notification.Application.Queries.GetNotificationById;
using HRMS.Services.Notification.Application.Queries.GetUnreadCount;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Notification.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NotificationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyNotifications(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string? type = null,
        [FromQuery] string? category = null,
        [FromQuery] bool? isRead = null,
        [FromQuery] string? searchTerm = null)
    {
        var userId = GetUserId();
        var query = new GetMyNotificationsQuery
        {
            UserId = userId,
            Page = page,
            PageSize = pageSize,
            Type = type != null && Enum.TryParse<Domain.Enums.NotificationType>(type, true, out var t) ? t : null,
            Category = category != null && Enum.TryParse<Domain.Enums.NotificationCategory>(category, true, out var c) ? c : null,
            IsRead = isRead,
            SearchTerm = searchTerm
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("unread-count")]
    public async Task<IActionResult> GetUnreadCount()
    {
        var userId = GetUserId();
        var count = await _mediator.Send(new GetUnreadCountQuery { UserId = userId });
        return Ok(new { count });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var userId = GetUserId();
        var result = await _mediator.Send(new GetNotificationByIdQuery { Id = id, UserId = userId });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPut("{id:guid}/read")]
    public async Task<IActionResult> MarkAsRead(Guid id)
    {
        var userId = GetUserId();
        await _mediator.Send(new MarkAsReadCommand { NotificationId = id, UserId = userId });
        return Ok();
    }

    [HttpPut("read-all")]
    public async Task<IActionResult> MarkAllAsRead()
    {
        var userId = GetUserId();
        var count = await _mediator.Send(new MarkAllAsReadCommand { UserId = userId });
        return Ok(new { marked = count });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = GetUserId();
        await _mediator.Send(new DeleteNotificationCommand { NotificationId = id, UserId = userId });
        return Ok();
    }

    private Guid GetUserId()
    {
        var claim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        if (claim == null) throw new UnauthorizedAccessException();
        return Guid.Parse(claim.Value);
    }
}
