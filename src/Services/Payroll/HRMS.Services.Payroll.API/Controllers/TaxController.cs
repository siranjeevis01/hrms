using HRMS.Services.Payroll.Application.Commands.SubmitTaxDeclaration;
using HRMS.Services.Payroll.Application.Commands.UpdateTaxConfiguration;
using HRMS.Services.Payroll.Application.Commands.VerifyTaxDeclaration;
using HRMS.Services.Payroll.Application.Queries.GetEmployeeTaxDeclaration;
using HRMS.Services.Payroll.Application.Queries.GetTaxConfiguration;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Payroll.API.Controllers;

[ApiController]
[Route("api/payroll/tax")]
public class TaxController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaxController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("configuration")]
    public async Task<ActionResult> GetConfiguration([FromQuery] GetTaxConfigurationQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPut("configuration")]
    public async Task<ActionResult<Guid>> UpdateConfiguration([FromBody] UpdateTaxConfigurationCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpPost("declaration")]
    public async Task<ActionResult<Guid>> SubmitDeclaration([FromBody] SubmitTaxDeclarationCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpGet("declaration")]
    public async Task<ActionResult> GetDeclaration([FromQuery] GetEmployeeTaxDeclarationQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPut("{id:guid}/verify")]
    public async Task<ActionResult> Verify(Guid id, [FromBody] VerifyTaxDeclarationCommand command)
    {
        command.DeclarationId = id;
        await _mediator.Send(command);
        return NoContent();
    }
}
