using HRMS.Services.Employee.Application.Commands.AddWorkExperience;
using HRMS.Services.Employee.Application.Queries.GetEmployeeExperience;
using HRMS.Services.Employee.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Employee.API.Controllers;

[ApiController]
[Route("api/employees/[controller]")]
public class EmployeeExperienceController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeeExperienceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("employee/{id:guid}")]
    [ProducesResponseType(typeof(List<WorkExperienceDto>), 200)]
    public async Task<IActionResult> GetExperience(Guid id)
    {
        var result = await _mediator.Send(new GetEmployeeExperienceQuery { EmployeeId = id });
        return Ok(result);
    }

    [HttpPost("employee/{id:guid}")]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> AddExperience(Guid id, [FromBody] AddWorkExperienceCommand command)
    {
        command.EmployeeId = id;
        var expId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetExperience), new { id }, expId);
    }

    [HttpPut("{expId:guid}")]
    [ProducesResponseType(204)]
    public IActionResult UpdateExperience(Guid expId)
    {
        return NoContent();
    }

    [HttpDelete("{expId:guid}")]
    [ProducesResponseType(204)]
    public IActionResult DeleteExperience(Guid expId)
    {
        return NoContent();
    }
}
