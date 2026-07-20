using HRMS.Services.Employee.Application.Commands.AddDependent;
using HRMS.Services.Employee.Application.Queries.GetEmployeeDependents;
using HRMS.Services.Employee.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Employee.API.Controllers;

[ApiController]
[Route("api/employees/[controller]")]
public class EmployeeDependentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeeDependentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("employee/{id:guid}")]
    [ProducesResponseType(typeof(List<DependentDto>), 200)]
    public async Task<IActionResult> GetDependents(Guid id)
    {
        var result = await _mediator.Send(new GetEmployeeDependentsQuery { EmployeeId = id });
        return Ok(result);
    }

    [HttpPost("employee/{id:guid}")]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> AddDependent(Guid id, [FromBody] AddDependentCommand command)
    {
        command.EmployeeId = id;
        var dependentId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetDependents), new { id }, dependentId);
    }

    [HttpPut("{dependentId:guid}")]
    [ProducesResponseType(204)]
    public IActionResult UpdateDependent(Guid dependentId)
    {
        return NoContent();
    }

    [HttpDelete("{dependentId:guid}")]
    [ProducesResponseType(204)]
    public IActionResult DeleteDependent(Guid dependentId)
    {
        return NoContent();
    }
}
