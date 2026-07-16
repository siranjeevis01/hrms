using HRMS.Services.Payroll.Application.Commands.GeneratePayslip;
using HRMS.Services.Payroll.Application.Queries.GetPayslip;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Payroll.API.Controllers;

[ApiController]
[Route("api/payroll/payslips")]
public class PayslipController : ControllerBase
{
    private readonly IMediator _mediator;

    public PayslipController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{employeeId:guid}/{month:int}/{year:int}")]
    public async Task<ActionResult> GetPayslip(Guid employeeId, int month, int year)
    {
        var result = await _mediator.Send(new GetPayslipQuery
        {
            EmployeeId = employeeId,
            Month = month,
            Year = year
        });

        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost("generate")]
    public async Task<ActionResult<Guid>> Generate([FromBody] GeneratePayslipCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }
}
