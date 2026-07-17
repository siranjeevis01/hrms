using HRMS.Services.Notification.Application.Queries.GetDeliveryLogs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Notification.API.Controllers;

[ApiController]
[Route("api/notifications/[controller]")]
[Authorize]
public class NotificationDeliveryLogController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationDeliveryLogController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetDeliveryLogs(
        [FromQuery] Guid? notificationId = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _mediator.Send(new GetDeliveryLogsQuery
        {
            NotificationId = notificationId,
            Page = page,
            PageSize = pageSize
        });
        return Ok(result);
    }
}
