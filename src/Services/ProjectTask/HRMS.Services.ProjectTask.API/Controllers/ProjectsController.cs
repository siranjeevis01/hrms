using HRMS.Services.ProjectTask.Application.Commands.AddProjectMember;
using HRMS.Services.ProjectTask.Application.Commands.ChangeProjectStatus;
using HRMS.Services.ProjectTask.Application.Commands.CreateProject;
using HRMS.Services.ProjectTask.Application.Commands.RemoveProjectMember;
using HRMS.Services.ProjectTask.Application.Commands.UpdateProject;
using HRMS.Services.ProjectTask.Application.DTOs;
using HRMS.Services.ProjectTask.Application.Queries.GetProject;
using HRMS.Services.ProjectTask.Application.Queries.GetProjectMembers;
using HRMS.Services.ProjectTask.Application.Queries.GetProjects;
using HRMS.Services.ProjectTask.Application.Queries.GetProjectStats;
using HRMS.Shared.Kernel.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.ProjectTask.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<ProjectListDto>), 200)]
    public async Task<IActionResult> GetProjects(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Domain.Enums.ProjectStatus? status = null,
        [FromQuery] Guid? departmentId = null,
        [FromQuery] string? searchTerm = null,
        [FromQuery] string? sortBy = null)
    {
        var query = new GetProjectsQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            Status = status,
            DepartmentId = departmentId,
            SearchTerm = searchTerm,
            SortBy = sortBy
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProjectDto), 200)]
    public async Task<IActionResult> GetProject(Guid id)
    {
        var result = await _mediator.Send(new GetProjectQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetProject), new { id }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateProject(Guid id, [FromBody] UpdateProjectCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id:guid}/status")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ChangeStatus(Guid id, [FromBody] ChangeProjectStatusCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("{id:guid}/members")]
    [ProducesResponseType(typeof(List<ProjectMemberDto>), 200)]
    public async Task<IActionResult> GetMembers(Guid id)
    {
        var result = await _mediator.Send(new GetProjectMembersQuery { ProjectId = id });
        return Ok(result);
    }

    [HttpPost("{id:guid}/members")]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> AddMember(Guid id, [FromBody] AddProjectMemberCommand command)
    {
        command.ProjectId = id;
        var memberId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetMembers), new { id }, memberId);
    }

    [HttpDelete("{id:guid}/members/{memberId:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> RemoveMember(Guid id, Guid memberId)
    {
        await _mediator.Send(new RemoveProjectMemberCommand { ProjectId = id, MemberId = memberId });
        return NoContent();
    }

    [HttpGet("{id:guid}/stats")]
    [ProducesResponseType(typeof(ProjectStatsDto), 200)]
    public async Task<IActionResult> GetStats(Guid id)
    {
        var result = await _mediator.Send(new GetProjectStatsQuery { ProjectId = id });
        return Ok(result);
    }
}
