using MediatR;
using Microsoft.AspNetCore.Mvc;
using HRMS.Services.Organization.Application.Commands.CreateHoliday;
using HRMS.Services.Organization.Application.Commands.Delete;
using HRMS.Services.Organization.Application.Queries.GetHolidays;

namespace HRMS.Services.Organization.API.Controllers;

[ApiController]
[Route("api/organization/[controller]")]
[Produces("application/json")]
public class HolidayController : ControllerBase
{
    private readonly IMediator _mediator;

    public HolidayController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Application.DTOs.HolidayDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetHolidays(
        [FromQuery] Guid companyId,
        [FromQuery] int? year,
        [FromQuery] Guid? branchId,
        [FromQuery] bool? isActive,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetHolidaysQuery
        {
            CompanyId = companyId,
            Year = year,
            BranchId = branchId,
            IsActive = isActive
        }, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Application.DTOs.HolidayDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateHolidayCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetHolidays), new { companyId = result.CompanyId }, result);
    }

    [HttpPost("bulk")]
    [ProducesResponseType(typeof(List<Application.DTOs.HolidayDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> BulkCreate([FromBody] List<CreateHolidayCommand> commands, CancellationToken cancellationToken)
    {
        var results = new List<Application.DTOs.HolidayDto>();

        foreach (var command in commands)
        {
            var result = await _mediator.Send(command, cancellationToken);
            results.Add(result);
        }

        return CreatedAtAction(nameof(GetHolidays), new { companyId = results.First().CompanyId }, results);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteCommand
        {
            Id = id,
            EntityType = nameof(EntityType.Holiday)
        }, cancellationToken);

        if (!result)
            return NotFound(new { message = $"Holiday with ID {id} not found." });

        return NoContent();
    }
}
