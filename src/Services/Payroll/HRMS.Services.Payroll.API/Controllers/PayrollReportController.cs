using HRMS.Services.Payroll.Application.Queries.GetPayrollCostAnalysis;
using HRMS.Services.Payroll.Application.Queries.GetPayrollSummary;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Payroll.API.Controllers;

[ApiController]
[Route("api/payroll/reports")]
public class PayrollReportController : ControllerBase
{
    private readonly IMediator _mediator;

    public PayrollReportController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("summary")]
    public async Task<ActionResult> GetSummary([FromQuery] GetPayrollSummaryQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("cost-analysis")]
    public async Task<ActionResult> GetCostAnalysis([FromQuery] GetPayrollCostAnalysisQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
