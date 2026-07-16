using HRMS.Services.Attendance.Application.Commands.BulkMarkAttendance;
using HRMS.Services.Attendance.Application.Commands.CheckIn;
using HRMS.Services.Attendance.Application.Commands.CheckOut;
using HRMS.Services.Attendance.Application.Commands.ManualCheckIn;
using HRMS.Services.Attendance.Application.Queries.GetAttendanceReport;
using HRMS.Services.Attendance.Application.Queries.GetAttendanceSummary;
using HRMS.Services.Attendance.Application.Queries.GetEmployeeAttendance;
using HRMS.Services.Attendance.Application.Queries.GetLateComers;
using HRMS.Services.Attendance.Application.Queries.GetMonthlyAttendance;
using HRMS.Services.Attendance.Application.Queries.GetTeamAttendance;
using HRMS.Services.Attendance.Application.Queries.GetTodayAttendance;
using HRMS.Services.Attendance.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Attendance.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttendanceController : ControllerBase
{
    private readonly IMediator _mediator;

    public AttendanceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("check-in")]
    [ProducesResponseType(typeof(Guid), 201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CheckIn([FromBody] CheckInCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTodayAttendance), new { employeeId = command.EmployeeId }, new { id });
    }

    [HttpPost("check-out")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CheckOut([FromBody] CheckOutCommand command)
    {
        await _mediator.Send(command);
        return Ok(new { message = "Checked out successfully." });
    }

    [HttpPost("manual")]
    [ProducesResponseType(typeof(Guid), 201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> ManualCheckIn([FromBody] ManualCheckInCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTodayAttendance), new { employeeId = command.EmployeeId }, new { id });
    }

    [HttpGet("today/{employeeId:guid}")]
    [ProducesResponseType(typeof(Application.DTOs.TodayAttendanceDto), 200)]
    public async Task<IActionResult> GetTodayAttendance(Guid employeeId)
    {
        var result = await _mediator.Send(new GetTodayAttendanceQuery { EmployeeId = employeeId });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("employee/{employeeId:guid}")]
    [ProducesResponseType(typeof(List<Application.DTOs.AttendanceRecordDto>), 200)]
    public async Task<IActionResult> GetEmployeeAttendance(
        Guid employeeId,
        [FromQuery] DateTime fromDate,
        [FromQuery] DateTime toDate)
    {
        var result = await _mediator.Send(new GetEmployeeAttendanceQuery
        {
            EmployeeId = employeeId,
            FromDate = fromDate,
            ToDate = toDate
        });
        return Ok(result);
    }

    [HttpGet("monthly/{employeeId:guid}")]
    [ProducesResponseType(typeof(Application.DTOs.AttendanceSummaryDto), 200)]
    public async Task<IActionResult> GetMonthlyAttendance(
        Guid employeeId,
        [FromQuery] int year,
        [FromQuery] int month)
    {
        var result = await _mediator.Send(new GetMonthlyAttendanceQuery
        {
            EmployeeId = employeeId,
            Year = year,
            Month = month
        });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("team")]
    [ProducesResponseType(typeof(List<Application.DTOs.AttendanceRecordDto>), 200)]
    public async Task<IActionResult> GetTeamAttendance(
        [FromQuery] Guid managerId,
        [FromQuery] DateTime date)
    {
        var result = await _mediator.Send(new GetTeamAttendanceQuery
        {
            ManagerId = managerId,
            Date = date
        });
        return Ok(result);
    }

    [HttpGet("summary/{employeeId:guid}")]
    [ProducesResponseType(typeof(List<Application.DTOs.AttendanceSummaryDto>), 200)]
    public async Task<IActionResult> GetAttendanceSummary(Guid employeeId)
    {
        var result = await _mediator.Send(new GetAttendanceSummaryQuery { EmployeeId = employeeId });
        return Ok(result);
    }

    [HttpGet("late-comers")]
    [ProducesResponseType(typeof(List<Application.DTOs.AttendanceRecordDto>), 200)]
    public async Task<IActionResult> GetLateComers(
        [FromQuery] DateTime date,
        [FromQuery] Guid? departmentId = null)
    {
        var result = await _mediator.Send(new GetLateComersQuery
        {
            Date = date,
            DepartmentId = departmentId
        });
        return Ok(result);
    }

    [HttpGet("report")]
    [ProducesResponseType(typeof(List<Application.DTOs.AttendanceReportDto>), 200)]
    public async Task<IActionResult> GetAttendanceReport(
        [FromQuery] Guid? departmentId,
        [FromQuery] DateTime fromDate,
        [FromQuery] DateTime toDate)
    {
        var result = await _mediator.Send(new GetAttendanceReportQuery
        {
            DepartmentId = departmentId,
            FromDate = fromDate,
            ToDate = toDate
        });
        return Ok(result);
    }

    [HttpPost("bulk")]
    [ProducesResponseType(typeof(int), 200)]
    public async Task<IActionResult> BulkMarkAttendance([FromBody] BulkMarkAttendanceCommand command)
    {
        var count = await _mediator.Send(command);
        return Ok(new { recordsCreated = count });
    }
}
