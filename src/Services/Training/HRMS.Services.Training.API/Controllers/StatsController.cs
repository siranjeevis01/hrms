using HRMS.Services.Training.Application.DTOs;
using HRMS.Services.Training.Application.Queries.GetTrainingStats;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Training.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StatsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(TrainingStatsDto), 200)]
    public async Task<IActionResult> GetTrainingStats()
    {
        var result = await _mediator.Send(new GetTrainingStatsQuery());
        return Ok(result);
    }
}
