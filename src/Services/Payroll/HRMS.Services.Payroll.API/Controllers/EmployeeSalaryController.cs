using HRMS.Services.Payroll.Application.Commands.AssignSalaryComponent;
using HRMS.Services.Payroll.Application.Queries.GetEmployeeSalaryBreakdown;
using HRMS.Services.Payroll.Application.Queries.GetEmployeePayroll;
using HRMS.Services.Payroll.Application.Queries.GetEmployeePayrollHistory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Payroll.API.Controllers;

[ApiController]
[Route("api/payroll/employee-salary")]
public class EmployeeSalaryController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeeSalaryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{employeeId:guid}/breakdown")]
    public async Task<ActionResult> GetBreakdown(Guid employeeId, [FromQuery] GetEmployeeSalaryBreakdownQuery query)
    {
        query.EmployeeId = employeeId;
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("assign")]
    public async Task<ActionResult<Guid>> AssignComponent([FromBody] AssignSalaryComponentCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpGet("{employeeId:guid}/payroll")]
    public async Task<ActionResult> GetEmployeePayroll(Guid employeeId, [FromQuery] int month, [FromQuery] int year)
    {
        var result = await _mediator.Send(new GetEmployeePayrollQuery
        {
            EmployeeId = employeeId,
            Month = month,
            Year = year
        });
        return Ok(result);
    }

    [HttpGet("{employeeId:guid}/history")]
    public async Task<ActionResult> GetHistory(Guid employeeId, [FromQuery] GetEmployeePayrollHistoryQuery query)
    {
        query.EmployeeId = employeeId;
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
