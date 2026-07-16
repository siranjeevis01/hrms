using HRMS.Services.Expense.Application.Commands.CreateExpensePolicy;
using HRMS.Services.Expense.Application.Commands.UpdateExpensePolicy;
using HRMS.Services.Expense.Application.Queries.GetExpensePolicies;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Expense.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensePoliciesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExpensePoliciesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ExpensePolicyDto>), 200)]
    public async Task<IActionResult> GetExpensePolicies(
        [FromQuery] string tenantId = "",
        [FromQuery] bool? isActive = null)
    {
        var result = await _mediator.Send(new GetExpensePoliciesQuery
        {
            TenantId = tenantId,
            IsActive = isActive
        });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateExpensePolicy([FromBody] CreateExpensePolicyCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetExpensePolicies), new { tenantId = command.TenantId }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateExpensePolicy(Guid id, [FromBody] UpdateExpensePolicyCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }
}
