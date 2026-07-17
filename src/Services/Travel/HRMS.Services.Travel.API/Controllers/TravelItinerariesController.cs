using HRMS.Services.Travel.Application.Commands.AddItinerary;
using HRMS.Services.Travel.Application.Commands.UpdateItinerary;
using HRMS.Services.Travel.Application.Queries.GetItinerary;
using HRMS.Services.Travel.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Travel.API.Controllers;

[ApiController]
[Route("api/travel-requests/{travelRequestId:guid}/itineraries")]
public class TravelItinerariesController : ControllerBase
{
    private readonly IMediator _mediator;

    public TravelItinerariesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<TravelItineraryDto>), 200)]
    public async Task<IActionResult> GetItinerary(Guid travelRequestId)
    {
        var result = await _mediator.Send(new GetItineraryQuery { TravelRequestId = travelRequestId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> AddItinerary(Guid travelRequestId, [FromBody] AddItineraryCommand command)
    {
        command.TravelRequestId = travelRequestId;
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetItinerary), new { travelRequestId }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateItinerary(Guid id, [FromBody] UpdateItineraryCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }
}
