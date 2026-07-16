using HRMS.Services.Notification.Application.Commands.UpdatePreference;
using HRMS.Services.Notification.Application.Queries.GetPreferences;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Notification.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NotificationPreferenceController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationPreferenceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetPreferences()
    {
        var userId = GetUserId();
        var result = await _mediator.Send(new GetPreferencesQuery { UserId = userId });
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePreference([FromBody] UpdateNotificationPreferenceCommand command)
    {
        var userId = GetUserId();
        command = command with { UserId = userId };
        await _mediator.Send(command);
        return Ok();
    }

    private Guid GetUserId()
    {
        var claim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        if (claim == null) throw new UnauthorizedAccessException();
        return Guid.Parse(claim.Value);
    }
}
