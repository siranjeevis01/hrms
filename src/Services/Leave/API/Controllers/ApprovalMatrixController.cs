using HRMS.Services.Leave.Application.Commands.CreateApprovalMatrix;
using HRMS.Services.Leave.Application.Queries.GetApprovalMatrix;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Leave.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApprovalMatrixController : ControllerBase
{
    private readonly IMediator _mediator;

    public ApprovalMatrixController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetApprovalMatrixQuery query) =>
        Ok(await _mediator.Send(query));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateApprovalMatrixCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAll), new { }, id);
    }
}
