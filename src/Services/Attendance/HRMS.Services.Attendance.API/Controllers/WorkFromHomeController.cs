using HRMS.Services.Attendance.Application.Commands.ApproveWorkFromHome;
using HRMS.Services.Attendance.Application.Commands.RequestWorkFromHome;
using HRMS.Services.Attendance.Application.Queries.GetPendingWFH;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Attendance.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkFromHomeController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkFromHomeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> RequestWFH([FromBody] RequestWorkFromHomeCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetPending), new { id }, new { id });
    }

    [HttpPut("{id:guid}/approve")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Approve(Guid id, [FromQuery] Guid approvedBy, [FromQuery] bool isApproved = true)
    {
        await _mediator.Send(new ApproveWorkFromHomeCommand
        {
            WorkFromHomeId = id,
            ApprovedBy = approvedBy,
            IsApproved = isApproved
        });
        return Ok(new { message = isApproved ? "WFH approved." : "WFH rejected." });
    }

    [HttpGet("pending")]
    [ProducesResponseType(typeof(List<Application.DTOs.WorkFromHomeDto>), 200)]
    public async Task<IActionResult> GetPending([FromQuery] Guid? tenantId = null)
    {
        var result = await _mediator.Send(new GetPendingWFHQuery { TenantId = tenantId });
        return Ok(result);
    }
}
