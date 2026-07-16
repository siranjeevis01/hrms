using HRMS.Services.Recruitment.Application.Commands.CreateCandidate;
using HRMS.Services.Recruitment.Application.Commands.CreateReferral;
using HRMS.Services.Recruitment.Application.Commands.RejectCandidate;
using HRMS.Services.Recruitment.Application.Commands.UpdateCandidate;
using HRMS.Services.Recruitment.Application.DTOs;
using HRMS.Services.Recruitment.Application.Queries.GetCandidate;
using HRMS.Services.Recruitment.Application.Queries.GetCandidates;
using HRMS.Services.Recruitment.Application.Queries.GetCandidateApplications;
using HRMS.Services.Recruitment.Application.Queries.GetReferrals;
using HRMS.Shared.Kernel.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Recruitment.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CandidatesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CandidatesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<CandidateDto>), 200)]
    public async Task<IActionResult> GetCandidates(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Domain.Enums.CandidateStatus? status = null,
        [FromQuery] Domain.Enums.CandidateSource? source = null,
        [FromQuery] string? searchTerm = null,
        [FromQuery] Guid? tenantId = null)
    {
        var query = new GetCandidatesQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            Status = status,
            Source = source,
            SearchTerm = searchTerm,
            TenantId = tenantId
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CandidateDto), 200)]
    public async Task<IActionResult> GetCandidate(Guid id)
    {
        var result = await _mediator.Send(new GetCandidateQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateCandidate([FromBody] CreateCandidateCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetCandidate), new { id }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateCandidate(Guid id, [FromBody] UpdateCandidateCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/reject")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> RejectCandidate(Guid id, [FromBody] RejectCandidateCommand command)
    {
        command.CandidateId = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("{id:guid}/applications")]
    [ProducesResponseType(typeof(List<JobApplicationDto>), 200)]
    public async Task<IActionResult> GetCandidateApplications(Guid id)
    {
        var result = await _mediator.Send(new GetCandidateApplicationsQuery { CandidateId = id });
        return Ok(result);
    }

    [HttpGet("referrals/{employeeId:guid}")]
    [ProducesResponseType(typeof(List<CandidateDto>), 200)]
    public async Task<IActionResult> GetReferrals(Guid employeeId)
    {
        var result = await _mediator.Send(new GetReferralsQuery { EmployeeId = employeeId });
        return Ok(result);
    }

    [HttpPost("referral")]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateReferral([FromBody] CreateReferralCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetCandidate), new { id }, id);
    }
}
