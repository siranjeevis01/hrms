using HRMS.Services.Expense.Application.Commands.AddExpenseItem;
using HRMS.Services.Expense.Application.Commands.RemoveExpenseItem;
using HRMS.Services.Expense.Application.Commands.UpdateExpenseItem;
using HRMS.Services.Expense.Application.Queries.GetExpenseItems;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Expense.API.Controllers;

[ApiController]
[Route("api/expense-claims/{claimId:guid}/items")]
public class ExpenseItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExpenseItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ExpenseItemDto>), 200)]
    public async Task<IActionResult> GetExpenseItems(Guid claimId)
    {
        var result = await _mediator.Send(new GetExpenseItemsQuery { ClaimId = claimId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> AddExpenseItem(Guid claimId, [FromBody] AddExpenseItemCommand command)
    {
        command.ClaimId = claimId;
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetExpenseItems), new { claimId }, id);
    }

    [HttpPut("{itemId:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateExpenseItem(Guid claimId, Guid itemId, [FromBody] UpdateExpenseItemCommand command)
    {
        command.Id = itemId;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{itemId:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> RemoveExpenseItem(Guid claimId, Guid itemId)
    {
        await _mediator.Send(new RemoveExpenseItemCommand { ClaimId = claimId, ItemId = itemId });
        return NoContent();
    }
}
