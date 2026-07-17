using HRMS.Services.Attendance.Application.Commands.AssignShift;
using HRMS.Services.Attendance.Application.Queries.GetShiftAssignments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Attendance.API.Controllers;

[ApiController]
[Route("api/attendance/[controller]")]
public class ShiftAssignmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShiftAssignmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Application.DTOs.ShiftAssignmentDto>), 200)]
    public async Task<IActionResult> GetAll([FromQuery] Guid? employeeId = null)
    {
        var result = await _mediator.Send(new GetShiftAssignmentsQuery { EmployeeId = employeeId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> Assign([FromBody] AssignShiftCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAll), new { employeeId = command.EmployeeId }, new { id });
    }
}
