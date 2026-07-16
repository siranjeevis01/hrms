using MediatR;
using Microsoft.AspNetCore.Mvc;
using HRMS.Services.Organization.Application.Commands.CreateShift;
using HRMS.Services.Organization.Application.Commands.Delete;
using HRMS.Services.Organization.Application.Queries.GetShifts;

namespace HRMS.Services.Organization.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ShiftController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShiftController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<Application.DTOs.ShiftDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetShifts(
        [FromQuery] Guid? companyId,
        [FromQuery] bool? isActive,
        [FromQuery] string? searchTerm,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetShiftsQuery
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
    [ProducesResponseType(typeof(Application.DTOs.ShiftDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateShiftCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetShifts), new { companyId = result.CompanyId }, result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteCommand
        {
            Id = id,
            EntityType = nameof(EntityType.Shift)
        }, cancellationToken);

        if (!result)
            return NotFound(new { message = $"Shift with ID {id} not found." });

        return NoContent();
    }
}
