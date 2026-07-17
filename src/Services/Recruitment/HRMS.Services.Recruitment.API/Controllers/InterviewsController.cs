using HRMS.Services.Recruitment.Application.Commands.CompleteInterview;
using HRMS.Services.Recruitment.Application.Commands.ScheduleInterview;
using HRMS.Services.Recruitment.Application.DTOs;
using HRMS.Services.Recruitment.Application.Queries.GetInterviews;
using HRMS.Services.Recruitment.Application.Queries.GetUpcomingInterviews;
using HRMS.Shared.Kernel.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Recruitment.API.Controllers;

[ApiController]
[Route("api/recruitment/[controller]")]
public class InterviewsController : ControllerBase
{
    private readonly IMediator _mediator;

    public InterviewsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<InterviewDto>), 200)]
    public async Task<IActionResult> GetInterviews(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Guid? candidateId = null,
        [FromQuery] Guid? jobPostingId = null,
        [FromQuery] Guid? tenantId = null)
    {
        var query = new GetInterviewsQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            CandidateId = candidateId,
            JobPostingId = jobPostingId,
            TenantId = tenantId
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("upcoming")]
    [ProducesResponseType(typeof(List<InterviewDto>), 200)]
    public async Task<IActionResult> GetUpcomingInterviews([FromQuery] Guid? tenantId = null)
    {
        var result = await _mediator.Send(new GetUpcomingInterviewsQuery { TenantId = tenantId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> ScheduleInterview([FromBody] ScheduleInterviewCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpPost("{id:guid}/complete")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> CompleteInterview(Guid id, [FromBody] CompleteInterviewCommand command)
    {
        command.InterviewId = id;
        await _mediator.Send(command);
        return NoContent();
    }
}
