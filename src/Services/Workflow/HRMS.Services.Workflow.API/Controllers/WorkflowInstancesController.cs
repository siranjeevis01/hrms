using HRMS.Services.Workflow.API.DTOs;
using HRMS.Services.Workflow.Application.Commands.AdvanceWorkflow;
using HRMS.Services.Workflow.Application.Commands.CancelWorkflow;
using HRMS.Services.Workflow.Application.Commands.RejectWorkflow;
using HRMS.Services.Workflow.Application.Commands.RerouteWorkflow;
using HRMS.Services.Workflow.Application.Commands.StartWorkflow;
using HRMS.Services.Workflow.Application.Queries.GetWorkflowInstance;
using HRMS.Services.Workflow.Application.Queries.GetWorkflowInstances;
using HRMS.Services.Workflow.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Workflow.API.Controllers;

[ApiController]
[Route("api/workflow/[controller]")]
public class WorkflowInstancesController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkflowInstancesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<WorkflowInstanceDto>), 200)]
    public async Task<IActionResult> GetWorkflowInstances(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] WorkflowEntityType? entityType = null,
        [FromQuery] WorkflowStatus? status = null,
        [FromQuery] Guid? requestedById = null,
        [FromQuery] string? searchTerm = null)
    {
        var query = new GetWorkflowInstancesQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            EntityType = entityType,
            Status = status,
            RequestedById = requestedById,
            SearchTerm = searchTerm
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(WorkflowInstanceDto), 200)]
    public async Task<IActionResult> GetWorkflowInstance(Guid id)
    {
        var result = await _mediator.Send(new GetWorkflowInstanceQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> StartWorkflow([FromBody] StartWorkflowCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetWorkflowInstance), new { id }, id);
    }

    [HttpPost("{id:guid}/advance")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> AdvanceWorkflow(Guid id, [FromQuery] Guid performedById)
    {
        await _mediator.Send(new AdvanceWorkflowCommand { InstanceId = id, PerformedById = performedById });
        return NoContent();
    }

    [HttpPost("{id:guid}/reject")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> RejectWorkflow(Guid id, [FromQuery] Guid performedById, [FromQuery] string? comments)
    {
        await _mediator.Send(new RejectWorkflowCommand { InstanceId = id, PerformedById = performedById, Comments = comments });
        return NoContent();
    }

    [HttpPost("{id:guid}/cancel")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> CancelWorkflow(Guid id, [FromQuery] Guid performedById, [FromQuery] string? comments)
    {
        await _mediator.Send(new CancelWorkflowCommand { InstanceId = id, PerformedById = performedById, Comments = comments });
        return NoContent();
    }

    [HttpPost("{id:guid}/reroute")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> RerouteWorkflow(Guid id, [FromQuery] Guid performedById, [FromQuery] Guid newApproverId, [FromQuery] string? comments)
    {
        await _mediator.Send(new RerouteWorkflowCommand
        {
            InstanceId = id,
            PerformedById = performedById,
            NewApproverId = newApproverId,
            Comments = comments
        });
        return NoContent();
    }
}
