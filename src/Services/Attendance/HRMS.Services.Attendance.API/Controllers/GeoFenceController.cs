using HRMS.Services.Attendance.Application.Commands.CreateGeoFence;
using HRMS.Services.Attendance.Application.Commands.UpdateGeoFence;
using HRMS.Services.Attendance.Application.Queries.GetGeoFences;
using HRMS.Services.Attendance.Application.Queries.ValidateGeoLocation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Attendance.API.Controllers;

[ApiController]
[Route("api/attendance/[controller]")]
public class GeoFenceController : ControllerBase
{
    private readonly IMediator _mediator;

    public GeoFenceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Application.DTOs.GeoFenceDto>), 200)]
    public async Task<IActionResult> GetAll([FromQuery] Guid companyId)
    {
        var result = await _mediator.Send(new GetGeoFencesQuery { CompanyId = companyId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> Create([FromBody] CreateGeoFenceCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAll), new { companyId = command.CompanyId }, new { id });
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGeoFenceCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return Ok(new { message = "Geo-fence updated successfully." });
    }

    [HttpPost("validate")]
    [ProducesResponseType(typeof(ValidateGeoLocationResult), 200)]
    public async Task<IActionResult> Validate([FromBody] ValidateGeoLocationQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
