using HRMS.Services.Recruitment.Application.Commands.CompleteOnboardingItem;
using HRMS.Services.Recruitment.Application.Commands.CreateOnboardingChecklist;
using HRMS.Services.Recruitment.Application.DTOs;
using HRMS.Services.Recruitment.Application.Queries.GetOnboardingChecklist;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Recruitment.API.Controllers;

[ApiController]
[Route("api/recruitment/[controller]")]
public class OnboardingChecklistsController : ControllerBase
{
    private readonly IMediator _mediator;

    public OnboardingChecklistsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("by-employee/{employeeId:guid}")]
    [ProducesResponseType(typeof(List<OnboardingChecklistDto>), 200)]
    public async Task<IActionResult> GetByEmployee(Guid employeeId)
    {
        var result = await _mediator.Send(new GetOnboardingChecklistQuery { EmployeeId = employeeId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateChecklist([FromBody] CreateOnboardingChecklistCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpPost("{id:guid}/complete-item")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> CompleteItem(Guid id, [FromBody] CompleteOnboardingItemCommand command)
    {
        command.OnboardingChecklistId = id;
        await _mediator.Send(command);
        return NoContent();
    }
}
