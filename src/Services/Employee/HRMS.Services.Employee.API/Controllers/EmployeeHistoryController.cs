using HRMS.Services.Employee.Application.Queries.GetEmployeeHistory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Employee.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeHistoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeeHistoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("employee/{id:guid}")]
    [ProducesResponseType(typeof(List<EmployeeHistoryDto>), 200)]
    public async Task<IActionResult> GetHistory(Guid id)
    {
        var result = await _mediator.Send(new GetEmployeeHistoryQuery { EmployeeId = id });
        return Ok(result);
    }
}
