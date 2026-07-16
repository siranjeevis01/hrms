using MediatR;
using Microsoft.AspNetCore.Mvc;
using HRMS.Services.Organization.Application.Commands.CreateDesignation;
using HRMS.Services.Organization.Application.Commands.Delete;
using HRMS.Services.Organization.Application.Queries.GetDesignations;

namespace HRMS.Services.Organization.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class DesignationController : ControllerBase
{
    private readonly IMediator _mediator;

    public DesignationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<Application.DTOs.DesignationDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDesignations(
        [FromQuery] Guid? companyId,
        [FromQuery] bool? isActive,
        [FromQuery] string? searchTerm,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetDesignationsQuery
        {
            CompanyId = companyId,
            IsActive = isActive,
            SearchTerm = searchTerm,
            Page = page,
            PageSize = pageSize
        }, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Application.DTOs.DesignationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateDesignationCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetDesignations), new { companyId = result.CompanyId }, result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteCommand
        {
            Id = id,
            EntityType = nameof(EntityType.Designation)
        }, cancellationToken);

        if (!result)
            return NotFound(new { message = $"Designation with ID {id} not found." });

        return NoContent();
    }
}
