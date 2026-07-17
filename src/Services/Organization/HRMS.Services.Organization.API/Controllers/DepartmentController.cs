using MediatR;
using Microsoft.AspNetCore.Mvc;
using HRMS.Services.Organization.Application.Commands.CreateDepartment;
using HRMS.Services.Organization.Application.Commands.UpdateDepartment;
using HRMS.Services.Organization.Application.Commands.Delete;
using HRMS.Services.Organization.Application.Queries.GetDepartments;
using HRMS.Services.Organization.Application.Queries.GetDepartmentTree;

namespace HRMS.Services.Organization.API.Controllers;

[ApiController]
[Route("api/organization/[controller]")]
[Produces("application/json")]
public class DepartmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public DepartmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Application.DTOs.DepartmentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDepartments(
        [FromQuery] Guid? companyId,
        [FromQuery] Guid? branchId,
        [FromQuery] bool? isActive,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetDepartmentsQuery
        {
            CompanyId = companyId,
            BranchId = branchId,
            IsActive = isActive
        }, cancellationToken);

        return Ok(result);
    }

    [HttpGet("tree/{companyId:guid}")]
    [ProducesResponseType(typeof(List<Application.DTOs.DepartmentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDepartmentTree(Guid companyId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetDepartmentTreeQuery
        {
            CompanyId = companyId
        }, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Application.DTOs.DepartmentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateDepartmentCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetDepartments), new { companyId = result.CompanyId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Application.DTOs.DepartmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDepartmentCommand command, CancellationToken cancellationToken)
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
            EntityType = nameof(EntityType.Department)
        }, cancellationToken);

        if (!result)
            return NotFound(new { message = $"Department with ID {id} not found." });

        return NoContent();
    }
}
