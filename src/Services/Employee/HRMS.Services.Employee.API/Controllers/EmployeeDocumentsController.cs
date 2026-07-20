using HRMS.Services.Employee.Application.Commands.UploadDocument;
using HRMS.Services.Employee.Application.Commands.UpdateDocument;
using HRMS.Services.Employee.Application.Commands.DeleteDocument;
using HRMS.Services.Employee.Application.Commands.VerifyDocument;
using HRMS.Services.Employee.Application.Queries.GetEmployeeDocuments;
using HRMS.Services.Employee.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Employee.API.Controllers;

[ApiController]
[Route("api/employees/[controller]")]
public class EmployeeDocumentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeeDocumentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("employee/{id:guid}")]
    [ProducesResponseType(typeof(List<EmployeeDocumentDto>), 200)]
    public async Task<IActionResult> GetDocuments(Guid id)
    {
        var result = await _mediator.Send(new GetEmployeeDocumentsQuery { EmployeeId = id });
        return Ok(result);
    }

    [HttpPost("employee/{id:guid}")]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> UploadDocument(Guid id, [FromBody] UploadDocumentCommand command)
    {
        command.EmployeeId = id;
        var docId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetDocuments), new { id }, docId);
    }

    [HttpPut("{docId:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateDocument(Guid docId, [FromBody] UpdateDocumentCommand command)
    {
        command.Id = docId;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{docId:guid}/verify")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> VerifyDocument(Guid docId, [FromBody] VerifyDocumentCommand command)
    {
        command.DocumentId = docId;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{docId:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteDocument(Guid docId)
    {
        await _mediator.Send(new DeleteDocumentCommand { Id = docId });
        return NoContent();
    }
}
