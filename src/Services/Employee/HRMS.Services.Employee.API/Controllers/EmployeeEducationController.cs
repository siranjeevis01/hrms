using HRMS.Services.Employee.Application.Commands.AddEducation;
using HRMS.Services.Employee.Application.Queries.GetEmployeeEducation;
using HRMS.Services.Employee.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Employee.API.Controllers;

[ApiController]
[Route("api/employees/[controller]")]
public class EmployeeEducationController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeeEducationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("employee/{id:guid}")]
    [ProducesResponseType(typeof(List<EducationDto>), 200)]
    public async Task<IActionResult> GetEducation(Guid id)
    {
        var result = await _mediator.Send(new GetEmployeeEducationQuery { EmployeeId = id });
        return Ok(result);
    }

    [HttpPost("employee/{id:guid}")]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> AddEducation(Guid id, [FromBody] AddEducationCommand command)
    {
        command.EmployeeId = id;
        var eduId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetEducation), new { id }, eduId);
    }

    [HttpPut("{eduId:guid}")]
    [ProducesResponseType(204)]
    public IActionResult UpdateEducation(Guid eduId)
    {
        return NoContent();
    }

    [HttpDelete("{eduId:guid}")]
    [ProducesResponseType(204)]
    public IActionResult DeleteEducation(Guid eduId)
    {
        return NoContent();
    }
}
