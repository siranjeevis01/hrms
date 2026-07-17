using HRMS.Services.Travel.Application.Commands.ApproveTravelRequest;
using HRMS.Services.Travel.Application.Commands.CancelTravelRequest;
using HRMS.Services.Travel.Application.Commands.CompleteTravelRequest;
using HRMS.Services.Travel.Application.Commands.CreateTravelRequest;
using HRMS.Services.Travel.Application.Commands.RejectTravelRequest;
using HRMS.Services.Travel.Application.Commands.SubmitTravelRequest;
using HRMS.Services.Travel.Application.Commands.UpdateTravelRequest;
using HRMS.Services.Travel.Application.Queries.GetEmployeeTravelRequests;
using HRMS.Services.Travel.Application.Queries.GetTravelRequest;
using HRMS.Services.Travel.Application.Queries.GetTravelRequests;
using HRMS.Services.Travel.Application.Queries.GetTravelStats;
using HRMS.Services.Travel.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Travel.API.Controllers;

[ApiController]
[Route("api/travel/[controller]")]
public class TravelRequestsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TravelRequestsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<TravelRequestDto>), 200)]
    public async Task<IActionResult> GetTravelRequests(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Guid? employeeId = null,
        [FromQuery] Domain.Enums.TravelRequestStatus? status = null,
        [FromQuery] string? destination = null,
        [FromQuery] DateTime? fromDate = null,
        [FromQuery] DateTime? toDate = null,
        [FromQuery] string? searchTerm = null)
    {
        var query = new GetTravelRequestsQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            EmployeeId = employeeId,
            Status = status,
            Destination = destination,
            FromDate = fromDate,
            ToDate = toDate,
            SearchTerm = searchTerm
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TravelRequestDto), 200)]
    public async Task<IActionResult> GetTravelRequest(Guid id)
    {
        var result = await _mediator.Send(new GetTravelRequestQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateTravelRequest([FromBody] CreateTravelRequestCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTravelRequest), new { id }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateTravelRequest(Guid id, [FromBody] UpdateTravelRequestCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/submit")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> SubmitTravelRequest(Guid id)
    {
        await _mediator.Send(new SubmitTravelRequestCommand { Id = id });
        return NoContent();
    }

    [HttpPost("{id:guid}/approve")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ApproveTravelRequest(Guid id)
    {
        await _mediator.Send(new ApproveTravelRequestCommand { Id = id });
        return NoContent();
    }

    [HttpPost("{id:guid}/reject")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> RejectTravelRequest(Guid id)
    {
        await _mediator.Send(new RejectTravelRequestCommand { Id = id });
        return NoContent();
    }

    [HttpPost("{id:guid}/complete")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> CompleteTravelRequest(Guid id)
    {
        await _mediator.Send(new CompleteTravelRequestCommand { Id = id });
        return NoContent();
    }

    [HttpPost("{id:guid}/cancel")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> CancelTravelRequest(Guid id)
    {
        await _mediator.Send(new CancelTravelRequestCommand { Id = id });
        return NoContent();
    }

    [HttpGet("employee/{employeeId:guid}")]
    [ProducesResponseType(typeof(PagedResult<TravelRequestDto>), 200)]
    public async Task<IActionResult> GetEmployeeTravelRequests(
        Guid employeeId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Domain.Enums.TravelRequestStatus? status = null)
    {
        var query = new GetEmployeeTravelRequestsQuery
        {
            EmployeeId = employeeId,
            PageNumber = pageNumber,
            PageSize = pageSize,
            Status = status
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("stats")]
    [ProducesResponseType(typeof(TravelStatsDto), 200)]
    public async Task<IActionResult> GetTravelStats(
        [FromQuery] Guid? employeeId = null,
        [FromQuery] DateTime? fromDate = null,
        [FromQuery] DateTime? toDate = null,
        [FromQuery] string tenantId = "")
    {
        var result = await _mediator.Send(new GetTravelStatsQuery
        {
            EmployeeId = employeeId,
            FromDate = fromDate,
            ToDate = toDate,
            TenantId = tenantId
        });
        return Ok(result);
    }
}
