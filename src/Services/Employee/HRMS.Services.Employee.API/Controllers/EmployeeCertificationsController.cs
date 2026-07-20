using HRMS.Services.Employee.Application.Commands.AddCertification;
using HRMS.Services.Employee.Application.Commands.UpdateCertification;
using HRMS.Services.Employee.Application.Commands.DeleteCertification;
using HRMS.Services.Employee.Application.Queries.GetEmployeeCertifications;
using HRMS.Services.Employee.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Employee.API.Controllers;

[ApiController]
[Route("api/employees/[controller]")]
public class EmployeeCertificationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeeCertificationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("employee/{id:guid}")]
    [ProducesResponseType(typeof(List<CertificationDto>), 200)]
    public async Task<IActionResult> GetCertifications(Guid id)
    {
        var result = await _mediator.Send(new GetEmployeeCertificationsQuery { EmployeeId = id });
        return Ok(result);
    }

    [HttpPost("employee/{id:guid}")]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> AddCertification(Guid id, [FromBody] AddCertificationCommand command)
    {
        command.EmployeeId = id;
        var certId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetCertifications), new { id }, certId);
    }

    [HttpPut("{certId:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateCertification(Guid certId, [FromBody] UpdateCertificationCommand command)
    {
        command.Id = certId;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{certId:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteCertification(Guid certId)
    {
        await _mediator.Send(new DeleteCertificationCommand { Id = certId });
        return NoContent();
    }
}
