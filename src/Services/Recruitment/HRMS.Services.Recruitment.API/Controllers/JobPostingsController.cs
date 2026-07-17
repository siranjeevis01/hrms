using HRMS.Services.Recruitment.Application.Commands.CloseJobPosting;
using HRMS.Services.Recruitment.Application.Commands.CreateJobPosting;
using HRMS.Services.Recruitment.Application.Commands.PublishJobPosting;
using HRMS.Services.Recruitment.Application.Commands.UpdateJobPosting;
using HRMS.Services.Recruitment.Application.DTOs;
using HRMS.Services.Recruitment.Application.Queries.GetJobPosting;
using HRMS.Services.Recruitment.Application.Queries.GetJobPostings;
using HRMS.Shared.Kernel.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Recruitment.API.Controllers;

[ApiController]
[Route("api/recruitment/[controller]")]
public class JobPostingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public JobPostingsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<JobPostingDto>), 200)]
    public async Task<IActionResult> GetJobPostings(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Domain.Enums.JobStatus? status = null,
        [FromQuery] Guid? departmentId = null,
        [FromQuery] string? searchTerm = null,
        [FromQuery] Guid? tenantId = null)
    {
        var query = new GetJobPostingsQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            Status = status,
            DepartmentId = departmentId,
            SearchTerm = searchTerm,
            TenantId = tenantId
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(JobPostingDto), 200)]
    public async Task<IActionResult> GetJobPosting(Guid id)
    {
        var result = await _mediator.Send(new GetJobPostingQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateJobPosting([FromBody] CreateJobPostingCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetJobPosting), new { id }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateJobPosting(Guid id, [FromBody] UpdateJobPostingCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/publish")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> PublishJobPosting(Guid id)
    {
        await _mediator.Send(new PublishJobPostingCommand { JobPostingId = id });
        return NoContent();
    }

    [HttpPost("{id:guid}/close")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> CloseJobPosting(Guid id)
    {
        await _mediator.Send(new CloseJobPostingCommand { JobPostingId = id });
        return NoContent();
    }
}
