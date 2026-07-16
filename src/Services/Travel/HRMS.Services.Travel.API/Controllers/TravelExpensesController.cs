using HRMS.Services.Travel.Application.Commands.AddTravelExpense;
using HRMS.Services.Travel.Application.Queries.GetTravelExpenses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Travel.API.Controllers;

[ApiController]
[Route("api/travel-requests/{travelRequestId:guid}/expenses")]
public class TravelExpensesController : ControllerBase
{
    private readonly IMediator _mediator;

    public TravelExpensesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<TravelExpenseDto>), 200)]
    public async Task<IActionResult> GetTravelExpenses(Guid travelRequestId)
    {
        var result = await _mediator.Send(new GetTravelExpensesQuery { TravelRequestId = travelRequestId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> AddTravelExpense(Guid travelRequestId, [FromBody] AddTravelExpenseCommand command)
    {
        command.TravelRequestId = travelRequestId;
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTravelExpenses), new { travelRequestId }, id);
    }
}
