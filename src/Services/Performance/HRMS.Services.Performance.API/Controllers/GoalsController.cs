using HRMS.Services.Performance.Application.Commands.CompleteGoal;
using HRMS.Services.Performance.Application.Commands.CreateGoal;
using HRMS.Services.Performance.Application.Commands.StartGoal;
using HRMS.Services.Performance.Application.Commands.UpdateGoal;
using HRMS.Services.Performance.Application.Commands.UpdateGoalProgress;
using HRMS.Services.Performance.Application.Queries.GetEmployeeGoals;
using HRMS.Services.Performance.Application.Queries.GetGoal;
using HRMS.Services.Performance.Application.Queries.GetGoals;
using HRMS.Services.Performance.Application.DTOs;
using HRMS.Services.Performance.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Performance.API.Controllers;

[ApiController]
[Route("api/performance/[controller]")]
public class GoalsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GoalsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<GoalDto>), 200)]
    public async Task<IActionResult> GetGoals(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] GoalCategory? category = null,
        [FromQuery] GoalStatus? status = null,
        [FromQuery] Guid? departmentId = null,
        [FromQuery] string? searchTerm = null,
        [FromQuery] string tenantId = "")
    {
        var query = new GetGoalsQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            Category = category,
            Status = status,
            DepartmentId = departmentId,
            SearchTerm = searchTerm,
            TenantId = tenantId
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GoalDto), 200)]
    public async Task<IActionResult> GetGoal(Guid id)
    {
        var result = await _mediator.Send(new GetGoalQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("employee/{employeeId:guid}")]
    [ProducesResponseType(typeof(List<GoalDto>), 200)]
    public async Task<IActionResult> GetEmployeeGoals(Guid employeeId, [FromQuery] string tenantId = "")
    {
        var result = await _mediator.Send(new GetEmployeeGoalsQuery { EmployeeId = employeeId, TenantId = tenantId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateGoal([FromBody] CreateGoalCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetGoal), new { id }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateGoal(Guid id, [FromBody] UpdateGoalCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/start")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> StartGoal(Guid id)
    {
        await _mediator.Send(new StartGoalCommand { GoalId = id });
        return NoContent();
    }

    [HttpPost("{id:guid}/complete")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> CompleteGoal(Guid id)
    {
        await _mediator.Send(new CompleteGoalCommand { GoalId = id });
        return NoContent();
    }

    [HttpPut("{id:guid}/progress")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateGoalProgress(Guid id, [FromBody] UpdateGoalProgressCommand command)
    {
        command.GoalId = id;
        await _mediator.Send(command);
        return NoContent();
    }
}
