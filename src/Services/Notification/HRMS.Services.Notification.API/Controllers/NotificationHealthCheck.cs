using HRMS.Services.Notification.Application.Queries.GetNotificationStats;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Notification.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NotificationStatsController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationStatsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetStats(
        [FromQuery] DateTime? fromDate = null,
        [FromQuery] DateTime? toDate = null)
    {
        var result = await _mediator.Send(new GetNotificationStatsQuery
        {
            FromDate = fromDate,
            ToDate = toDate
        });
        return Ok(result);
    }
}
