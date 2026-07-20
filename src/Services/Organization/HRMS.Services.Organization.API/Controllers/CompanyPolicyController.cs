using MediatR;
using Microsoft.AspNetCore.Mvc;
using HRMS.Services.Organization.Application.Commands.CreateCompanyPolicy;
using HRMS.Services.Organization.Application.Commands.UpdateCompanyPolicy;
using HRMS.Services.Organization.Application.Commands.Delete;
using HRMS.Services.Organization.Application.Queries.GetCompanyPolicies;

namespace HRMS.Services.Organization.API.Controllers;

[ApiController]
[Route("api/organization/[controller]")]
[Produces("application/json")]
public class CompanyPolicyController : ControllerBase
{
    private readonly IMediator _mediator;

    public CompanyPolicyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Application.DTOs.CompanyPolicyDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPolicies(
        [FromQuery] Guid companyId,
        [FromQuery] string? category,
        [FromQuery] bool? isActive,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCompanyPoliciesQuery
        {
            CompanyId = companyId,
            Category = category,
            IsActive = isActive
        }, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Application.DTOs.CompanyPolicyDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateCompanyPolicyCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetPolicies), new { companyId = result.CompanyId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Application.DTOs.CompanyPolicyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCompanyPolicyCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest(new { message = "ID mismatch between route and body." });

        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteCommand
        {
            Id = id,
            EntityType = nameof(EntityType.CompanyPolicy)
        }, cancellationToken);

        if (!result)
            return NotFound(new { message = $"Company policy with ID {id} not found." });

        return NoContent();
    }
}
