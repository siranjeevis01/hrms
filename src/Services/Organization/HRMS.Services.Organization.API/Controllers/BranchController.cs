using MediatR;
using Microsoft.AspNetCore.Mvc;
using HRMS.Services.Organization.Application.Commands.CreateBranch;
using HRMS.Services.Organization.Application.Commands.UpdateBranch;
using HRMS.Services.Organization.Application.Commands.Delete;
using HRMS.Services.Organization.Application.Queries.GetBranches;
using HRMS.Services.Organization.API.DTOs;

namespace HRMS.Services.Organization.API.Controllers;

[ApiController]
[Route("api/organization/[controller]")]
[Produces("application/json")]
public class BranchController : ControllerBase
{
    private readonly IMediator _mediator;

    public BranchController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<Application.DTOs.BranchDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBranches(
        [FromQuery] Guid? companyId,
        [FromQuery] bool? isActive,
        [FromQuery] string? searchTerm,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? sortBy = null,
        [FromQuery] bool sortDescending = false,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetBranchesQuery
        {
            CompanyId = companyId,
            IsActive = isActive,
            SearchTerm = searchTerm,
            Page = page,
            PageSize = pageSize,
            SortBy = sortBy,
            SortDescending = sortDescending
        }, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Application.DTOs.BranchDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateBranchCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetBranches), new { companyId = result.CompanyId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Application.DTOs.BranchDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBranchCommand command, CancellationToken cancellationToken)
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
            EntityType = nameof(EntityType.Branch)
        }, cancellationToken);

        if (!result)
            return NotFound(new { message = $"Branch with ID {id} not found." });

        return NoContent();
    }
}
