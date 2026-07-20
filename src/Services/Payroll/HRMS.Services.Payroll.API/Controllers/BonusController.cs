using HRMS.Services.Payroll.Application.Commands.ApproveBonus;
using HRMS.Services.Payroll.Application.Commands.GrantBonus;
using HRMS.Services.Payroll.Application.DTOs;
using HRMS.Services.Payroll.Application.Queries.GetBonuses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Payroll.API.Controllers;

[ApiController]
[Route("api/payroll/bonuses")]
public class BonusController : ControllerBase
{
    private readonly IMediator _mediator;

    public BonusController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<BonusDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<BonusDto>>> GetAll(
        [FromQuery] Guid? employeeId,
        [FromQuery] int? month,
        [FromQuery] int? year)
    {
        var result = await _mediator.Send(new GetBonusesQuery
        {
            EmployeeId = employeeId,
            Month = month,
            Year = year
        });
        return Ok(result);
    }

    [HttpPost("grant")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<ActionResult<Guid>> Grant([FromBody] GrantBonusCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpPost("{id:guid}/approve")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Approve(Guid id, [FromBody] ApproveBonusCommand command)
    {
        command.BonusId = id;
        await _mediator.Send(command);
        return NoContent();
    }
}
