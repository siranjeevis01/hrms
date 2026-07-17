using HRMS.Services.Workflow.API.DTOs;
using HRMS.Services.Workflow.Application.Commands.CreateDelegate;
using HRMS.Services.Workflow.Application.Commands.DeleteDelegate;
using HRMS.Services.Workflow.Application.Commands.UpdateDelegate;
using HRMS.Services.Workflow.Application.Queries.GetDelegates;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Workflow.API.Controllers;

[ApiController]
[Route("api/workflow/[controller]")]
public class DelegatesController : ControllerBase
{
    private readonly IMediator _mediator;

    public DelegatesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<DelegateDto>), 200)]
    public async Task<IActionResult> GetDelegates([FromQuery] Guid? userId = null)
    {
        var result = await _mediator.Send(new GetDelegatesQuery { UserId = userId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateDelegate([FromBody] CreateDelegateCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetDelegates), new { }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateDelegate(Guid id, [FromBody] UpdateDelegateCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteDelegate(Guid id)
    {
        await _mediator.Send(new DeleteDelegateCommand { Id = id });
        return NoContent();
    }
}
