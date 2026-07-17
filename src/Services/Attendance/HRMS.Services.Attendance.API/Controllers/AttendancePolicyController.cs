using HRMS.Services.Attendance.Application.Commands.UpdateAttendancePolicy;
using HRMS.Services.Attendance.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Attendance.API.Controllers;

[ApiController]
[Route("api/attendance/[controller]")]
public class AttendancePolicyController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IAttendanceDbContext _context;

    public AttendancePolicyController(IMediator mediator, IAttendanceDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Get([FromQuery] Guid companyId)
    {
        var policy = await _context.AttendancePolicies
            .FirstOrDefaultAsync(p => p.CompanyId == companyId);

        if (policy == null)
            return NotFound();

        return Ok(new
        {
            policy.Id,
            policy.CompanyId,
            policy.GracePeriodMinutes,
            policy.MaxLateAllowed,
            policy.LateDeductionMinutes,
            policy.AutoCheckoutTime,
            policy.HalfDayMinimumHours,
            policy.FullDayMinimumHours,
            policy.OvertimeEnabled,
            policy.OvertimeThresholdMinutes,
            policy.MaxOvertimeMinutes,
            policy.TenantId
        });
    }

    [HttpPut]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Update([FromBody] UpdateAttendancePolicyCommand command)
    {
        await _mediator.Send(command);
        return Ok(new { message = "Attendance policy updated successfully." });
    }
}
