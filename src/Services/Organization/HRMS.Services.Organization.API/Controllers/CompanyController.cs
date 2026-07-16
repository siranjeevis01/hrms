using MediatR;
using Microsoft.AspNetCore.Mvc;
using HRMS.Services.Organization.Application.Commands.CreateCompany;
using HRMS.Services.Organization.Application.Commands.UpdateCompany;
using HRMS.Services.Organization.Application.Commands.Delete;
using HRMS.Services.Organization.Application.Queries.GetCompany;

namespace HRMS.Services.Organization.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CompanyController : ControllerBase
{
    private readonly IMediator _mediator;

    public CompanyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Application.DTOs.CompanyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCompanyQuery { Id = id }, cancellationToken);
        if (result == null)
            return NotFound(new { message = $"Company with ID {id} not found." });

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Application.DTOs.CompanyDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateCompanyCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Application.DTOs.CompanyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCompanyCommand command, CancellationToken cancellationToken)
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
            EntityType = nameof(EntityType.Company)
        }, cancellationToken);

        if (!result)
            return NotFound(new { message = $"Company with ID {id} not found." });

        return NoContent();
    }
}
