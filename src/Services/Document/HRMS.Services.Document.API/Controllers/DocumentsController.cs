using HRMS.Services.Document.Application.Commands.UploadDocument;
using HRMS.Services.Document.Application.Commands.UpdateDocument;
using HRMS.Services.Document.Application.Commands.DeleteDocument;
using HRMS.Services.Document.Application.Commands.MoveDocument;
using HRMS.Services.Document.Application.Commands.ArchiveDocument;
using HRMS.Services.Document.Application.Commands.ShareDocument;
using HRMS.Services.Document.Application.Commands.RevokeShare;
using HRMS.Services.Document.Application.Commands.LogDocumentAccess;
using HRMS.Services.Document.Application.Queries.GetDocument;
using HRMS.Services.Document.Application.Queries.GetDocuments;
using HRMS.Services.Document.Application.Queries.GetDocumentVersions;
using HRMS.Services.Document.Application.Queries.GetDocumentShares;
using HRMS.Services.Document.Application.Queries.GetDocumentAccessLog;
using HRMS.Services.Document.Application.Queries.SearchDocuments;
using HRMS.Services.Document.Application.Queries.GetDocumentStats;
using HRMS.Services.Document.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Document.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DocumentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<DocumentDto>), 200)]
    public async Task<IActionResult> GetDocuments(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Guid? folderId = null,
        [FromQuery] Domain.Enums.DocumentStatus? status = null,
        [FromQuery] Domain.Enums.DocumentCategory? category = null,
        [FromQuery] string? searchTerm = null,
        [FromQuery] string? tenantId = null)
    {
        var query = new GetDocumentsQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            FolderId = folderId,
            Status = status,
            Category = category,
            SearchTerm = searchTerm,
            TenantId = tenantId
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(DocumentDto), 200)]
    public async Task<IActionResult> GetDocument(Guid id)
    {
        var result = await _mediator.Send(new GetDocumentQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> UploadDocument([FromBody] UploadDocumentCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetDocument), new { id }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateDocument(Guid id, [FromBody] UpdateDocumentCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteDocument(Guid id)
    {
        await _mediator.Send(new DeleteDocumentCommand { Id = id });
        return NoContent();
    }

    [HttpPut("{id:guid}/move")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> MoveDocument(Guid id, [FromBody] MoveDocumentCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id:guid}/archive")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ArchiveDocument(Guid id)
    {
        await _mediator.Send(new ArchiveDocumentCommand { Id = id });
        return NoContent();
    }

    [HttpGet("{id:guid}/versions")]
    [ProducesResponseType(typeof(List<DocumentVersionDto>), 200)]
    public async Task<IActionResult> GetDocumentVersions(Guid id)
    {
        var result = await _mediator.Send(new GetDocumentVersionsQuery { DocumentId = id });
        return Ok(result);
    }

    [HttpGet("{id:guid}/shares")]
    [ProducesResponseType(typeof(List<DocumentShareDto>), 200)]
    public async Task<IActionResult> GetDocumentShares(Guid id)
    {
        var result = await _mediator.Send(new GetDocumentSharesQuery { DocumentId = id });
        return Ok(result);
    }

    [HttpPost("{id:guid}/share")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ShareDocument(Guid id, [FromBody] ShareDocumentCommand command)
    {
        command.DocumentId = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}/shares/{shareId:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> RevokeShare(Guid id, Guid shareId)
    {
        await _mediator.Send(new RevokeShareCommand { DocumentId = id, ShareId = shareId });
        return NoContent();
    }

    [HttpPost("{id:guid}/access-log")]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> LogAccess(Guid id, [FromBody] LogDocumentAccessCommand command)
    {
        command.DocumentId = id;
        var logId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetDocumentAccessLog), new { documentId = id }, logId);
    }

    [HttpGet("access-log")]
    [ProducesResponseType(typeof(PagedResult<DocumentAccessLogDto>), 200)]
    public async Task<IActionResult> GetDocumentAccessLog(
        [FromQuery] Guid? documentId = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetDocumentAccessLogQuery
        {
            DocumentId = documentId,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("search")]
    [ProducesResponseType(typeof(PagedResult<DocumentDto>), 200)]
    public async Task<IActionResult> SearchDocuments(
        [FromQuery] string searchTerm,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Domain.Enums.DocumentCategory? category = null,
        [FromQuery] string? tenantId = null)
    {
        var query = new SearchDocumentsQuery
        {
            SearchTerm = searchTerm,
            PageNumber = pageNumber,
            PageSize = pageSize,
            Category = category,
            TenantId = tenantId
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("stats")]
    [ProducesResponseType(typeof(DocumentStatsDto), 200)]
    public async Task<IActionResult> GetDocumentStats([FromQuery] string? tenantId = null)
    {
        var result = await _mediator.Send(new GetDocumentStatsQuery { TenantId = tenantId });
        return Ok(result);
    }
}
