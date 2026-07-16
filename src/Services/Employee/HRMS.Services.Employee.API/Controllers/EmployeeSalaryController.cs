using HRMS.Services.Employee.Application.Commands.CreateSalaryStructure;
using HRMS.Services.Employee.Application.Queries.GetEmployeeHistory;
using HRMS.Services.Employee.Application.Queries.GetEmployeeSalary;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Employee.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeSalaryController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeeSalaryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("employee/{id:guid}")]
    [ProducesResponseType(typeof(SalaryStructureDto), 200)]
    public async Task<IActionResult> GetSalary(Guid id)
    {
        var result = await _mediator.Send(new GetEmployeeSalaryQuery { EmployeeId = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost("employee/{id:guid}")]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateSalary(Guid id, [FromBody] CreateSalaryStructureCommand command)
    {
        command.EmployeeId = id;
        var salaryId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetSalary), new { id }, salaryId);
    }

    [HttpGet("employee/{id:guid}/history")]
    [ProducesResponseType(typeof(List<EmployeeHistoryDto>), 200)]
    public async Task<IActionResult> GetSalaryHistory(Guid id)
    {
        var result = await _mediator.Send(new GetEmployeeHistoryQuery { EmployeeId = id });
        return Ok(result);
    }
}
