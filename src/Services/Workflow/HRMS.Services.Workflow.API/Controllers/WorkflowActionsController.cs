using HRMS.Services.Workflow.Application.Commands.TakeAction;
using HRMS.Services.Workflow.Application.Queries.GetWorkflowActions;
using HRMS.Services.Workflow.Application.Queries.GetWorkflowHistory;
using HRMS.Services.Workflow.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Workflow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkflowActionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkflowActionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("by-instance/{workflowInstanceId:guid}")]
    [ProducesResponseType(typeof(List<WorkflowActionDto>), 200)]
    public async Task<IActionResult> GetWorkflowActions(Guid workflowInstanceId)
    {
        var result = await _mediator.Send(new GetWorkflowActionsQuery { WorkflowInstanceId = workflowInstanceId });
        return Ok(result);
    }

    [HttpGet("history/{workflowInstanceId:guid}")]
    [ProducesResponseType(typeof(List<WorkflowActionDto>), 200)]
    public async Task<IActionResult> GetWorkflowHistory(Guid workflowInstanceId)
    {
        var result = await _mediator.Send(new GetWorkflowHistoryQuery { WorkflowInstanceId = workflowInstanceId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> TakeAction([FromBody] TakeActionCommand command)
    {
        await _mediator.Send(command);
        return StatusCode(201);
    }
}
