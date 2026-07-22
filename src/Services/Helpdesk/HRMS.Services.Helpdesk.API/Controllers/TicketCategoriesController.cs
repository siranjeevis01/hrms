using HRMS.Services.Helpdesk.Application.Commands.CreateTicketCategory;
using HRMS.Services.Helpdesk.Application.Commands.UpdateTicketCategory;
using HRMS.Services.Helpdesk.Application.Queries.GetTicketCategories;
using HRMS.Services.Helpdesk.Application.DTOs;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Helpdesk.API.Controllers;

[ApiController]
[Route("api/helpdesk/[controller]")]
public class TicketCategoriesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public TicketCategoriesController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<TicketCategoryDto>), 200)]
    public async Task<IActionResult> GetTicketCategories()
    {
        var tenantId = HttpContext.Request.Query["tenantId"].FirstOrDefault();
        if (string.IsNullOrEmpty(tenantId) && _currentUserService.TenantId.HasValue)
            tenantId = _currentUserService.TenantId.Value.ToString();

        var result = await _mediator.Send(new GetTicketCategoriesQuery { TenantId = tenantId ?? string.Empty });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateTicketCategory([FromBody] CreateTicketCategoryCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateTicketCategory(Guid id, [FromBody] UpdateTicketCategoryCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }
}
