using HRMS.Services.Workflow.API.DTOs;
using HRMS.Services.Workflow.Application.Commands.AddWorkflowStep;
using HRMS.Services.Workflow.Application.Commands.RemoveWorkflowStep;
using HRMS.Services.Workflow.Application.Commands.UpdateWorkflowStep;
using HRMS.Services.Workflow.Application.Queries.GetWorkflowSteps;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Workflow.API.Controllers;

[ApiController]
[Route("api/workflow/[controller]")]
public class WorkflowStepsController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkflowStepsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("by-definition/{workflowDefinitionId:guid}")]
    [ProducesResponseType(typeof(List<WorkflowStepDto>), 200)]
    public async Task<IActionResult> GetWorkflowSteps(Guid workflowDefinitionId)
    {
        var result = await _mediator.Send(new GetWorkflowStepsQuery { WorkflowDefinitionId = workflowDefinitionId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> AddWorkflowStep([FromBody] AddWorkflowStepCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetWorkflowSteps), new { workflowDefinitionId = command.WorkflowDefinitionId }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateWorkflowStep(Guid id, [FromBody] UpdateWorkflowStepCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> RemoveWorkflowStep(Guid id, [FromQuery] Guid workflowDefinitionId)
    {
        await _mediator.Send(new RemoveWorkflowStepCommand { Id = id, WorkflowDefinitionId = workflowDefinitionId });
        return NoContent();
    }
}
