using HRMS.Services.Helpdesk.Application.Commands.CreateTicketCategory;
using HRMS.Services.Helpdesk.Application.Commands.UpdateTicketCategory;
using HRMS.Services.Helpdesk.Application.Queries.GetTicketCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Helpdesk.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketCategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public TicketCategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<TicketCategoryDto>), 200)]
    public async Task<IActionResult> GetTicketCategories([FromQuery] string tenantId = "")
    {
        var result = await _mediator.Send(new GetTicketCategoriesQuery { TenantId = tenantId });
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
