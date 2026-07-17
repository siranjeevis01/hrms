using HRMS.Services.Workflow.API.DTOs;
using HRMS.Services.Workflow.Application.Commands.ActivateWorkflowDefinition;
using HRMS.Services.Workflow.Application.Commands.CreateWorkflowDefinition;
using HRMS.Services.Workflow.Application.Commands.DeactivateWorkflowDefinition;
using HRMS.Services.Workflow.Application.Commands.UpdateWorkflowDefinition;
using HRMS.Services.Workflow.Application.Queries.GetWorkflowDefinition;
using HRMS.Services.Workflow.Application.Queries.GetWorkflowDefinitions;
using HRMS.Services.Workflow.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Workflow.API.Controllers;

[ApiController]
[Route("api/workflow/[controller]")]
public class WorkflowDefinitionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkflowDefinitionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<WorkflowDefinitionDto>), 200)]
    public async Task<IActionResult> GetWorkflowDefinitions(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] WorkflowEntityType? entityType = null,
        [FromQuery] bool? isActive = null,
        [FromQuery] string? searchTerm = null)
    {
        var query = new GetWorkflowDefinitionsQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            EntityType = entityType,
            IsActive = isActive,
            SearchTerm = searchTerm
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(WorkflowDefinitionDto), 200)]
    public async Task<IActionResult> GetWorkflowDefinition(Guid id)
    {
        var result = await _mediator.Send(new GetWorkflowDefinitionQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateWorkflowDefinition([FromBody] CreateWorkflowDefinitionCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetWorkflowDefinition), new { id }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateWorkflowDefinition(Guid id, [FromBody] UpdateWorkflowDefinitionCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/activate")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ActivateWorkflowDefinition(Guid id)
    {
        await _mediator.Send(new ActivateWorkflowDefinitionCommand { Id = id });
        return NoContent();
    }

    [HttpPost("{id:guid}/deactivate")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeactivateWorkflowDefinition(Guid id)
    {
        await _mediator.Send(new DeactivateWorkflowDefinitionCommand { Id = id });
        return NoContent();
    }
}
