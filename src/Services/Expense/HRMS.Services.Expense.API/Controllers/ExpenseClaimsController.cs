using HRMS.Services.Expense.Application.Commands.ApproveExpenseClaim;
using HRMS.Services.Expense.Application.Commands.CreateExpenseClaim;
using HRMS.Services.Expense.Application.Commands.RejectExpenseClaim;
using HRMS.Services.Expense.Application.Commands.SubmitExpenseClaim;
using HRMS.Services.Expense.Application.Commands.UpdateExpenseClaim;
using HRMS.Services.Expense.Application.Queries.GetExpenseClaim;
using HRMS.Services.Expense.Application.Queries.GetExpenseClaims;
using HRMS.Services.Expense.Application.Queries.GetEmployeeExpenseClaims;
using HRMS.Services.Expense.Application.Queries.GetExpenseStats;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Expense.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpenseClaimsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExpenseClaimsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<ExpenseClaimDto>), 200)]
    public async Task<IActionResult> GetExpenseClaims(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Guid? employeeId = null,
        [FromQuery] Domain.Enums.ClaimStatus? status = null,
        [FromQuery] DateTime? fromDate = null,
        [FromQuery] DateTime? toDate = null,
        [FromQuery] string? searchTerm = null)
    {
        var query = new GetExpenseClaimsQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            EmployeeId = employeeId,
            Status = status,
            FromDate = fromDate,
            ToDate = toDate,
            SearchTerm = searchTerm
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ExpenseClaimDto), 200)]
    public async Task<IActionResult> GetExpenseClaim(Guid id)
    {
        var result = await _mediator.Send(new GetExpenseClaimQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateExpenseClaim([FromBody] CreateExpenseClaimCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetExpenseClaim), new { id }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateExpenseClaim(Guid id, [FromBody] UpdateExpenseClaimCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/submit")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> SubmitExpenseClaim(Guid id)
    {
        await _mediator.Send(new SubmitExpenseClaimCommand { Id = id });
        return NoContent();
    }

    [HttpPost("{id:guid}/approve")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ApproveExpenseClaim(Guid id, [FromBody] ApproveExpenseClaimCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/reject")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> RejectExpenseClaim(Guid id, [FromBody] RejectExpenseClaimCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("employee/{employeeId:guid}")]
    [ProducesResponseType(typeof(PagedResult<ExpenseClaimDto>), 200)]
    public async Task<IActionResult> GetEmployeeExpenseClaims(
        Guid employeeId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Domain.Enums.ClaimStatus? status = null)
    {
        var query = new GetEmployeeExpenseClaimsQuery
        {
            EmployeeId = employeeId,
            PageNumber = pageNumber,
            PageSize = pageSize,
            Status = status
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("stats")]
    [ProducesResponseType(typeof(ExpenseStatsDto), 200)]
    public async Task<IActionResult> GetExpenseStats(
        [FromQuery] Guid? employeeId = null,
        [FromQuery] DateTime? fromDate = null,
        [FromQuery] DateTime? toDate = null,
        [FromQuery] string tenantId = "")
    {
        var result = await _mediator.Send(new GetExpenseStatsQuery
        {
            EmployeeId = employeeId,
            FromDate = fromDate,
            ToDate = toDate,
            TenantId = tenantId
        });
        return Ok(result);
    }
}
