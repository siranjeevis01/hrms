using HRMS.Services.Employee.Application.Interfaces;
using HRMS.Services.Leave.Application.Commands.AdjustLeaveBalance;
using HRMS.Services.Leave.Application.Commands.AllocateLeaveBalance;
using HRMS.Services.Leave.Application.Queries.GetMyLeaveBalance;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Leave.API.Controllers;

[ApiController]
[Route("api/leave/[controller]")]
public class LeaveBalanceController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;
    private readonly IEmployeeDbContext _employeeDb;

    public LeaveBalanceController(IMediator mediator, ICurrentUserService currentUserService, IEmployeeDbContext employeeDb)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
        _employeeDb = employeeDb;
    }

    [HttpGet]
    public async Task<IActionResult> GetBalance([FromQuery] Guid? employeeId, [FromQuery] int? year)
    {
        var effectiveEmployeeId = employeeId ?? Guid.Empty;

        if (effectiveEmployeeId == Guid.Empty && _currentUserService.UserId.HasValue)
        {
            var employee = await _employeeDb.Employees
                .FirstOrDefaultAsync(e => e.UserId == _currentUserService.UserId.Value);
            if (employee != null)
                effectiveEmployeeId = employee.Id;
        }

        var query = new GetMyLeaveBalanceQuery
        {
            EmployeeId = effectiveEmployeeId,
            Year = year,
            TenantId = _currentUserService.TenantId
        };

        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("allocate")]
    public async Task<IActionResult> Allocate([FromBody] AllocateLeaveBalanceCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPost("adjust")]
    public async Task<IActionResult> Adjust([FromBody] AdjustLeaveBalanceCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}
