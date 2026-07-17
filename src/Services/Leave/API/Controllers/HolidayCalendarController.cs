using HRMS.Services.Leave.Application.Queries.GetHolidays;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Leave.API.Controllers;

[ApiController]
[Route("api/leave/[controller]")]
public class HolidayCalendarController : ControllerBase
{
    private readonly IMediator _mediator;

    public HolidayCalendarController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetHolidays([FromQuery] GetHolidaysQuery query) =>
        Ok(await _mediator.Send(query));
}
