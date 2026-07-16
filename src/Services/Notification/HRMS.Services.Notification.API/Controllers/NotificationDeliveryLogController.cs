using HRMS.Services.Notification.Application.Commands.CreateNotificationGroup;
using HRMS.Services.Notification.Application.Queries.GetNotificationGroups;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Notification.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NotificationGroupController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationGroupController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetGroups([FromQuery] bool? isActive = null)
    {
        var result = await _mediator.Send(new GetNotificationGroupsQuery { IsActive = isActive });
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateNotificationGroupCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetGroups), new { id }, new { id });
    }
}
