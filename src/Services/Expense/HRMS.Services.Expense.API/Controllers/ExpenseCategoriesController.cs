using HRMS.Services.Expense.Application.Queries.GetExpenseCategories;
using HRMS.Services.Expense.Application.DTOs;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Expense.API.Controllers;

[ApiController]
[Route("api/expenses/[controller]")]
public class ExpenseCategoriesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public ExpenseCategoriesController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ExpenseCategoryDto>), 200)]
    public async Task<IActionResult> GetExpenseCategories()
    {
        var tenantId = HttpContext.Request.Query["tenantId"].FirstOrDefault();
        if (string.IsNullOrEmpty(tenantId) && _currentUserService.TenantId.HasValue)
            tenantId = _currentUserService.TenantId.Value.ToString();

        var result = await _mediator.Send(new GetExpenseCategoriesQuery { TenantId = tenantId ?? string.Empty });
        return Ok(result);
    }
}
