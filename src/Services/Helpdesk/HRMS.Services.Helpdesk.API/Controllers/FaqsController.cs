using HRMS.Services.Helpdesk.Application.Commands.CreateFaq;
using HRMS.Services.Helpdesk.Application.Commands.UpdateFaq;
using HRMS.Services.Helpdesk.Application.Queries.GetFaqs;
using HRMS.Services.Helpdesk.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Helpdesk.API.Controllers;

[ApiController]
[Route("api/helpdesk/[controller]")]
public class FaqsController : ControllerBase
{
    private readonly IMediator _mediator;

    public FaqsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<FaqDto>), 200)]
    public async Task<IActionResult> GetFaqs(
        [FromQuery] Guid? categoryId = null,
        [FromQuery] string tenantId = "")
    {
        var result = await _mediator.Send(new GetFaqsQuery
        {
            CategoryId = categoryId,
            TenantId = tenantId
        });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateFaq([FromBody] CreateFaqCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateFaq(Guid id, [FromBody] UpdateFaqCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }
}
