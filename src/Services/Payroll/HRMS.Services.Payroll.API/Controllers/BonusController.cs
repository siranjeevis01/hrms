using HRMS.Services.Payroll.Application.Commands.ApproveBonus;
using HRMS.Services.Payroll.Application.Commands.GrantBonus;
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

    [HttpPost("grant")]
    public async Task<ActionResult<Guid>> Grant([FromBody] GrantBonusCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpPost("{id:guid}/approve")]
    public async Task<ActionResult> Approve(Guid id, [FromBody] ApproveBonusCommand command)
    {
        command.BonusId = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    public ActionResult GetAll()
    {
        // Implement get bonuses query
        return Ok();
    }
}
