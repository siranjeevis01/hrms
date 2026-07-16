using HRMS.Services.Payroll.Application.Commands.ApprovePayroll;
using HRMS.Services.Payroll.Application.Commands.LockPayroll;
using HRMS.Services.Payroll.Application.Commands.ProcessPayroll;
using HRMS.Services.Payroll.Application.Commands.ReversePayroll;
using HRMS.Services.Payroll.Application.Queries.GetPayrollRunDetails;
using HRMS.Services.Payroll.Application.Queries.GetPayrollRuns;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Payroll.API.Controllers;

[ApiController]
[Route("api/payroll")]
public class PayrollController : ControllerBase
{
    private readonly IMediator _mediator;

    public PayrollController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("process")]
    public async Task<ActionResult<ProcessPayrollResult>> ProcessPayroll([FromBody] ProcessPayrollCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("{id:guid}/approve")]
    public async Task<ActionResult> ApprovePayroll(Guid id, [FromBody] ApprovePayrollCommand command)
    {
        command.PayrollRunId = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/lock")]
    public async Task<ActionResult> LockPayroll(Guid id, [FromBody] LockPayrollCommand command)
    {
        command.PayrollRunId = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/reverse")]
    public async Task<ActionResult> ReversePayroll(Guid id, [FromBody] ReversePayrollCommand command)
    {
        command.PayrollRunId = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("runs")]
    public async Task<ActionResult> GetPayrollRuns([FromQuery] GetPayrollRunsQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetPayrollRunDetails(Guid id)
    {
        var result = await _mediator.Send(new GetPayrollRunDetailsQuery { PayrollRunId = id });
        if (result == null) return NotFound();
        return Ok(result);
    }
}
