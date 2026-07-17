using HRMS.Services.Attendance.Application.Commands.CreateWifiNetwork;
using HRMS.Services.Attendance.Application.Queries.GetGeoFences;
using HRMS.Services.Attendance.Application.Queries.ValidateWifi;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Attendance.API.Controllers;

[ApiController]
[Route("api/attendance/[controller]")]
public class WifiNetworkController : ControllerBase
{
    private readonly IMediator _mediator;

    public WifiNetworkController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetAll([FromQuery] Guid companyId)
    {
        var result = await _mediator.Send(new GetGeoFencesQuery { CompanyId = companyId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> Create([FromBody] CreateWifiNetworkCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAll), new { companyId = command.CompanyId }, new { id });
    }

    [HttpPost("validate")]
    [ProducesResponseType(typeof(ValidateWifiResult), 200)]
    public async Task<IActionResult> Validate([FromBody] ValidateWifiQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
