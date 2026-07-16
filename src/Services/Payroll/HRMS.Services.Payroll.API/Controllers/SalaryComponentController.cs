using HRMS.Services.Payroll.Application.Commands.CreateSalaryComponent;
using HRMS.Services.Payroll.Application.Queries.GetSalaryComponents;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Payroll.API.Controllers;

[ApiController]
[Route("api/payroll/salary-components")]
public class SalaryComponentController : ControllerBase
{
    private readonly IMediator _mediator;

    public SalaryComponentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateSalaryComponentCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpGet]
    public async Task<ActionResult> GetAll([FromQuery] GetSalaryComponentsQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
