using HRMS.Services.Employee.Application.Commands.AddEmergencyContact;
using HRMS.Services.Employee.Application.Commands.UpdateEmergencyContact;
using HRMS.Services.Employee.Application.Queries.GetEmployeeEmergencyContacts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Employee.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmergencyContactsController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmergencyContactsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("employee/{id:guid}")]
    [ProducesResponseType(typeof(List<EmergencyContactDto>), 200)]
    public async Task<IActionResult> GetEmergencyContacts(Guid id)
    {
        var result = await _mediator.Send(new GetEmployeeEmergencyContactsQuery { EmployeeId = id });
        return Ok(result);
    }

    [HttpPost("employee/{id:guid}")]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> AddEmergencyContact(Guid id, [FromBody] AddEmergencyContactCommand command)
    {
        command.EmployeeId = id;
        var contactId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetEmergencyContacts), new { id }, contactId);
    }

    [HttpPut("{contactId:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateEmergencyContact(Guid contactId, [FromBody] UpdateEmergencyContactCommand command)
    {
        command.Id = contactId;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{contactId:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteEmergencyContact(Guid contactId)
    {
        return NoContent();
    }
}
