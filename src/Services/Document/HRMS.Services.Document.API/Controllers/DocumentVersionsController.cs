using HRMS.Services.Document.Application.Commands.CreateDocumentVersion;
using HRMS.Services.Document.Application.Queries.GetDocumentVersions;
using HRMS.Services.Document.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Document.API.Controllers;

[ApiController]
[Route("api/documents/{documentId:guid}/versions")]
public class DocumentVersionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DocumentVersionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<DocumentVersionDto>), 200)]
    public async Task<IActionResult> GetVersions(Guid documentId)
    {
        var result = await _mediator.Send(new GetDocumentVersionsQuery { DocumentId = documentId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(204)]
    public async Task<IActionResult> CreateVersion(Guid documentId, [FromBody] CreateDocumentVersionCommand command)
    {
        command.DocumentId = documentId;
        await _mediator.Send(command);
        return NoContent();
    }
}
