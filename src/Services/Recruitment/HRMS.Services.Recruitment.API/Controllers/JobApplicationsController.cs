using HRMS.Services.Recruitment.Application.Commands.ApplyForJob;
using HRMS.Services.Recruitment.Application.Commands.ScreenApplication;
using HRMS.Services.Recruitment.Application.Commands.ShortlistApplication;
using HRMS.Services.Recruitment.Application.DTOs;
using HRMS.Services.Recruitment.Application.Queries.GetCandidateApplications;
using HRMS.Services.Recruitment.Application.Queries.GetJobApplications;
using HRMS.Services.Recruitment.Application.Queries.GetPipelineSummary;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Recruitment.API.Controllers;

[ApiController]
[Route("api/recruitment/[controller]")]
public class JobApplicationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public JobApplicationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("by-job/{jobPostingId:guid}")]
    [ProducesResponseType(typeof(List<JobApplicationDto>), 200)]
    public async Task<IActionResult> GetByJobPosting(Guid jobPostingId)
    {
        var result = await _mediator.Send(new GetJobApplicationsQuery { JobPostingId = jobPostingId });
        return Ok(result);
    }

    [HttpGet("by-candidate/{candidateId:guid}")]
    [ProducesResponseType(typeof(List<JobApplicationDto>), 200)]
    public async Task<IActionResult> GetByCandidate(Guid candidateId)
    {
        var result = await _mediator.Send(new GetCandidateApplicationsQuery { CandidateId = candidateId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> ApplyForJob([FromBody] ApplyForJobCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetByJobPosting), new { jobPostingId = command.JobPostingId }, id);
    }

    [HttpPost("{id:guid}/screen")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ScreenApplication(Guid id, [FromBody] ScreenApplicationCommand command)
    {
        command.JobApplicationId = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/shortlist")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ShortlistApplication(Guid id, [FromBody] ShortlistApplicationCommand command)
    {
        command.JobApplicationId = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("pipeline")]
    [ProducesResponseType(typeof(List<PipelineSummaryDto>), 200)]
    public async Task<IActionResult> GetPipelineSummary(
        [FromQuery] Guid? jobPostingId = null,
        [FromQuery] Guid? tenantId = null)
    {
        var result = await _mediator.Send(new GetPipelineSummaryQuery
        {
            JobPostingId = jobPostingId,
            TenantId = tenantId
        });
        return Ok(result);
    }
}
