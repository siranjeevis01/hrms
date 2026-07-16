using HRMS.Services.Training.Application.Commands.IssueCertificate;
using HRMS.Services.Training.Application.DTOs;
using HRMS.Services.Training.Application.Queries.GetCertificate;
using HRMS.Services.Training.Application.Queries.GetEmployeeCertificates;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Training.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CertificatesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CertificatesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CertificateDto), 200)]
    public async Task<IActionResult> GetCertificate(Guid id)
    {
        var result = await _mediator.Send(new GetCertificateQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("by-employee/{employeeId:guid}")]
    [ProducesResponseType(typeof(List<CertificateDto>), 200)]
    public async Task<IActionResult> GetEmployeeCertificates(Guid employeeId)
    {
        var result = await _mediator.Send(new GetEmployeeCertificatesQuery { EmployeeId = employeeId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> IssueCertificate([FromBody] IssueCertificateCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetCertificate), new { id }, id);
    }
}
