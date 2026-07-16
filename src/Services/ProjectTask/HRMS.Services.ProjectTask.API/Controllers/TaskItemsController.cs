using HRMS.Services.ProjectTask.Application.Commands.AssignTask;
using HRMS.Services.ProjectTask.Application.Commands.ChangeTaskStatus;
using HRMS.Services.ProjectTask.Application.Commands.CreateTask;
using HRMS.Services.ProjectTask.Application.Commands.UpdateTask;
using HRMS.Services.ProjectTask.Application.DTOs;
using HRMS.Services.ProjectTask.Application.Queries.GetTask;
using HRMS.Services.ProjectTask.Application.Queries.GetTaskComments;
using HRMS.Services.ProjectTask.Application.Queries.GetTaskTimeLogs;
using HRMS.Services.ProjectTask.Application.Queries.GetTasks;
using HRMS.Shared.Kernel.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskStatus = HRMS.Services.ProjectTask.Domain.Enums.TaskStatus;

namespace HRMS.Services.ProjectTask.API.Controllers;

[ApiController]
[Route("api/projects/{projectId:guid}/tasks")]
public class TaskItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<TaskItemDto>), 200)]
    public async Task<IActionResult> GetTasks(
        Guid projectId,
        [FromQuery] Guid? storyId,
        [FromQuery] Guid? assignedTo,
        [FromQuery] TaskStatus? status,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetTasksQuery
        {
            ProjectId = projectId,
            StoryId = storyId,
            AssignedTo = assignedTo,
            Status = status,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TaskItemDto), 200)]
    public async Task<IActionResult> GetTask(Guid projectId, Guid id)
    {
        var result = await _mediator.Send(new GetTaskQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateTask(Guid projectId, [FromBody] CreateTaskCommand command)
    {
        command.ProjectId = projectId;
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTask), new { projectId, id }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateTask(Guid projectId, Guid id, [FromBody] UpdateTaskCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/assign")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> AssignTask(Guid projectId, Guid id, [FromBody] AssignTaskCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id:guid}/status")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ChangeStatus(Guid projectId, Guid id, [FromBody] ChangeTaskStatusCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("{id:guid}/comments")]
    [ProducesResponseType(typeof(List<CommentDto>), 200)]
    public async Task<IActionResult> GetComments(Guid projectId, Guid id)
    {
        var result = await _mediator.Send(new GetTaskCommentsQuery { TaskItemId = id });
        return Ok(result);
    }

    [HttpGet("{id:guid}/timelogs")]
    [ProducesResponseType(typeof(List<TimeLogDto>), 200)]
    public async Task<IActionResult> GetTimeLogs(Guid projectId, Guid id)
    {
        var result = await _mediator.Send(new GetTaskTimeLogsQuery { TaskItemId = id });
        return Ok(result);
    }
}
