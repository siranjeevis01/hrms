using HRMS.Services.Document.Application.Commands.ShareDocument;
using HRMS.Services.Document.Application.Commands.RevokeShare;
using HRMS.Services.Document.Application.Queries.GetDocumentShares;
using HRMS.Services.Document.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Document.API.Controllers;

[ApiController]
[Route("api/documents/{documentId:guid}/shares")]
public class DocumentSharesController : ControllerBase
{
    private readonly IMediator _mediator;

    public DocumentSharesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<DocumentShareDto>), 200)]
    public async Task<IActionResult> GetShares(Guid documentId)
    {
        var result = await _mediator.Send(new GetDocumentSharesQuery { DocumentId = documentId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ShareDocument(Guid documentId, [FromBody] ShareDocumentCommand command)
    {
        command.DocumentId = documentId;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{shareId:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> RevokeShare(Guid documentId, Guid shareId)
    {
        await _mediator.Send(new RevokeShareCommand { DocumentId = documentId, ShareId = shareId });
        return NoContent();
    }
}
