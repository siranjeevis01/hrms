using HRMS.Services.Employee.Application.Commands.AddSkill;
using HRMS.Services.Employee.Application.Queries.GetEmployeeSkills;
using HRMS.Services.Employee.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Employee.API.Controllers;

[ApiController]
[Route("api/employees/[controller]")]
public class EmployeeSkillsController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeeSkillsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("employee/{id:guid}")]
    [ProducesResponseType(typeof(List<SkillDto>), 200)]
    public async Task<IActionResult> GetSkills(Guid id)
    {
        var result = await _mediator.Send(new GetEmployeeSkillsQuery { EmployeeId = id });
        return Ok(result);
    }

    [HttpPost("employee/{id:guid}")]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> AddSkill(Guid id, [FromBody] AddSkillCommand command)
    {
        command.EmployeeId = id;
        var skillId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetSkills), new { id }, skillId);
    }

    [HttpPut("{skillId:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateSkill(Guid skillId)
    {
        return NoContent();
    }

    [HttpDelete("{skillId:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteSkill(Guid skillId)
    {
        return NoContent();
    }
}
