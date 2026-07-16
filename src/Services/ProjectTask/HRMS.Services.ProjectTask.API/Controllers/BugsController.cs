using HRMS.Services.ProjectTask.Application.Commands.AssignBug;
using HRMS.Services.ProjectTask.Application.Commands.ChangeBugStatus;
using HRMS.Services.ProjectTask.Application.Commands.CreateBug;
using HRMS.Services.ProjectTask.Application.Commands.UpdateBug;
using HRMS.Services.ProjectTask.Application.DTOs;
using HRMS.Services.ProjectTask.Application.Queries.GetBugs;
using HRMS.Services.ProjectTask.Application.Queries.GetTaskComments;
using HRMS.Services.ProjectTask.Domain.Enums;
using HRMS.Shared.Kernel.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.ProjectTask.API.Controllers;

[ApiController]
[Route("api/projects/{projectId:guid}/bugs")]
public class BugsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BugsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<BugDto>), 200)]
    public async Task<IActionResult> GetBugs(
        Guid projectId,
        [FromQuery] Guid? assignedTo,
        [FromQuery] BugStatus? status,
        [FromQuery] BugSeverity? severity,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetBugsQuery
        {
            ProjectId = projectId,
            AssignedTo = assignedTo,
            Status = status,
            Severity = severity,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateBug(Guid projectId, [FromBody] CreateBugCommand command)
    {
        command.ProjectId = projectId;
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetBugs), new { projectId }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateBug(Guid projectId, Guid id, [FromBody] UpdateBugCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/assign")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> AssignBug(Guid projectId, Guid id, [FromBody] AssignBugCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id:guid}/status")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ChangeStatus(Guid projectId, Guid id, [FromBody] ChangeBugStatusCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("{id:guid}/comments")]
    [ProducesResponseType(typeof(List<CommentDto>), 200)]
    public async Task<IActionResult> GetComments(Guid projectId, Guid id)
    {
        var result = await _mediator.Send(new GetTaskCommentsQuery { BugId = id });
        return Ok(result);
    }
}
