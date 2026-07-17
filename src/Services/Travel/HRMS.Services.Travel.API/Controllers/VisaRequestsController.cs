using HRMS.Services.Travel.Application.Commands.CreateVisaRequest;
using HRMS.Services.Travel.Application.Commands.UpdateVisaRequest;
using HRMS.Services.Travel.Application.Queries.GetVisaRequests;
using HRMS.Services.Travel.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Travel.API.Controllers;

[ApiController]
[Route("api/travel/[controller]")]
public class VisaRequestsController : ControllerBase
{
    private readonly IMediator _mediator;

    public VisaRequestsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<VisaRequestDto>), 200)]
    public async Task<IActionResult> GetVisaRequests(
        [FromQuery] Guid? employeeId = null,
        [FromQuery] string tenantId = "")
    {
        var result = await _mediator.Send(new GetVisaRequestsQuery
        {
            EmployeeId = employeeId,
            TenantId = tenantId
        });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateVisaRequest([FromBody] CreateVisaRequestCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateVisaRequest(Guid id, [FromBody] UpdateVisaRequestCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }
}
