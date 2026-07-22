using HRMS.Services.Expense.Application.Queries.GetExpenseCategories;
using HRMS.Services.Expense.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Expense.API.Controllers;

[ApiController]
[Route("api/expenses/[controller]")]
public class ExpenseCategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExpenseCategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ExpenseCategoryDto>), 200)]
    public async Task<IActionResult> GetExpenseCategories()
    {
        var tenantId = HttpContext.Request.Query["tenantId"].FirstOrDefault() ?? "";
        if (string.IsNullOrEmpty(tenantId))
        {
            var tenantClaim = User.FindFirst("tenant_id")?.Value;
            if (!string.IsNullOrEmpty(tenantClaim))
                tenantId = tenantClaim;
        }
        var result = await _mediator.Send(new GetExpenseCategoriesQuery { TenantId = tenantId });
        return Ok(result);
    }
}
