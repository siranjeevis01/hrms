using HRMS.Services.Payroll.Application.Commands.ApproveLoan;
using HRMS.Services.Payroll.Application.Commands.RequestLoan;
using HRMS.Services.Payroll.Application.Queries.GetEmployeeLoans;
using HRMS.Services.Payroll.Application.Queries.GetLoanDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Payroll.API.Controllers;

[ApiController]
[Route("api/payroll/loans")]
public class LoanController : ControllerBase
{
    private readonly IMediator _mediator;

    public LoanController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("request")]
    public new async Task<ActionResult<Guid>> Request([FromBody] RequestLoanCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpPost("{id:guid}/approve")]
    public async Task<ActionResult> Approve(Guid id, [FromBody] ApproveLoanCommand command)
    {
        command.LoanId = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("employee/{employeeId:guid}")]
    public async Task<ActionResult> GetEmployeeLoans(Guid employeeId)
    {
        var result = await _mediator.Send(new GetEmployeeLoansQuery { EmployeeId = employeeId });
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetDetails(Guid id)
    {
        var result = await _mediator.Send(new GetLoanDetailsQuery { LoanId = id });
        if (result == null) return NotFound();
        return Ok(result);
    }
}
