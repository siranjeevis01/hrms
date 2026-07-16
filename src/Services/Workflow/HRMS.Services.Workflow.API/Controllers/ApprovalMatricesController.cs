using HRMS.Services.Workflow.Application.Commands.CreateApprovalMatrix;
using HRMS.Services.Workflow.Application.Commands.UpdateApprovalMatrix;
using HRMS.Services.Workflow.Application.Queries.GetApprovalMatrices;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Workflow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApprovalMatricesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ApprovalMatricesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ApprovalMatrixDto>), 200)]
    public async Task<IActionResult> GetApprovalMatrices([FromQuery] Guid? tenantId = null)
    {
        var result = await _mediator.Send(new GetApprovalMatricesQuery { TenantId = tenantId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateApprovalMatrix([FromBody] CreateApprovalMatrixCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetApprovalMatrices), new { }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateApprovalMatrix(Guid id, [FromBody] UpdateApprovalMatrixCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }
}
