using HRMS.Services.Workflow.API.DTOs;
using HRMS.Services.Workflow.Application.Commands.CreateNotificationRule;
using HRMS.Services.Workflow.Application.Commands.UpdateNotificationRule;
using HRMS.Services.Workflow.Application.Queries.GetNotificationRules;
using HRMS.Services.Workflow.Application.Queries.GetPendingApprovals;
using HRMS.Services.Workflow.Application.Queries.GetWorkflowStats;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Workflow.API.Controllers;

[ApiController]
[Route("api/workflow/[controller]")]
public class NotificationRulesController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationRulesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<NotificationRuleDto>), 200)]
    public async Task<IActionResult> GetNotificationRules([FromQuery] Guid? workflowDefinitionId = null)
    {
        var result = await _mediator.Send(new GetNotificationRulesQuery { WorkflowDefinitionId = workflowDefinitionId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateNotificationRule([FromBody] CreateNotificationRuleCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetNotificationRules), new { }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateNotificationRule(Guid id, [FromBody] UpdateNotificationRuleCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("pending-approvals/{employeeId:guid}")]
    [ProducesResponseType(typeof(List<PendingApprovalDto>), 200)]
    public async Task<IActionResult> GetPendingApprovals(Guid employeeId)
    {
        var result = await _mediator.Send(new GetPendingApprovalsQuery { EmployeeId = employeeId });
        return Ok(result);
    }

    [HttpGet("stats")]
    [ProducesResponseType(typeof(WorkflowStatsDto), 200)]
    public async Task<IActionResult> GetWorkflowStats()
    {
        var result = await _mediator.Send(new GetWorkflowStatsQuery());
        return Ok(result);
    }
}
