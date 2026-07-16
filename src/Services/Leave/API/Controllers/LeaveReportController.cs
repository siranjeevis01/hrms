using HRMS.Services.Leave.Application.Queries.GetLeaveReport;
using HRMS.Services.Leave.Application.Queries.GetTeamLeaveCalendar;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Leave.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaveReportController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveReportController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetReport([FromQuery] GetLeaveReportQuery query) =>
        Ok(await _mediator.Send(query));

    [HttpGet("calendar")]
    public async Task<IActionResult> GetCalendar([FromQuery] GetTeamLeaveCalendarQuery query) =>
        Ok(await _mediator.Send(query));
}
