using HRMS.Services.Performance.Application.Commands.AddReviewCriteria;
using HRMS.Services.Performance.Application.Commands.ApprovePerformanceReview;
using HRMS.Services.Performance.Application.Commands.CompletePerformanceReview;
using HRMS.Services.Performance.Application.Commands.CreatePerformanceReview;
using HRMS.Services.Performance.Application.Commands.SubmitPerformanceReview;
using HRMS.Services.Performance.Application.Queries.GetPerformanceReview;
using HRMS.Services.Performance.Application.Queries.GetPerformanceReviews;
using HRMS.Services.Performance.Application.DTOs;
using HRMS.Services.Performance.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Performance.API.Controllers;

[ApiController]
[Route("api/performance/[controller]")]
public class PerformanceReviewsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PerformanceReviewsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<PerformanceReviewDto>), 200)]
    public async Task<IActionResult> GetPerformanceReviews(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Guid? employeeId = null,
        [FromQuery] ReviewType? reviewType = null,
        [FromQuery] ReviewStatus? status = null,
        [FromQuery] string? reviewPeriod = null,
        [FromQuery] string tenantId = "")
    {
        var query = new GetPerformanceReviewsQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            EmployeeId = employeeId,
            ReviewType = reviewType,
            Status = status,
            ReviewPeriod = reviewPeriod,
            TenantId = tenantId
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PerformanceReviewDto), 200)]
    public async Task<IActionResult> GetPerformanceReview(Guid id)
    {
        var result = await _mediator.Send(new GetPerformanceReviewQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreatePerformanceReview([FromBody] CreatePerformanceReviewCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetPerformanceReview), new { id }, id);
    }

    [HttpPost("{id:guid}/submit")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> SubmitPerformanceReview(Guid id)
    {
        await _mediator.Send(new SubmitPerformanceReviewCommand { ReviewId = id });
        return NoContent();
    }

    [HttpPost("{id:guid}/complete")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> CompletePerformanceReview(Guid id, [FromBody] CompletePerformanceReviewCommand command)
    {
        command.ReviewId = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/approve")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ApprovePerformanceReview(Guid id)
    {
        await _mediator.Send(new ApprovePerformanceReviewCommand { ReviewId = id });
        return NoContent();
    }

    [HttpPost("{id:guid}/criteria")]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> AddReviewCriteria(Guid id, [FromBody] AddReviewCriteriaCommand command)
    {
        command.PerformanceReviewId = id;
        var criteriaId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetPerformanceReview), new { id }, criteriaId);
    }
}
