using HRMS.Services.Document.Application.Commands.CreateFolder;
using HRMS.Services.Document.Application.Commands.UpdateFolder;
using HRMS.Services.Document.Application.Commands.DeleteFolder;
using HRMS.Services.Document.Application.Queries.GetFolder;
using HRMS.Services.Document.Application.Queries.GetFolders;
using HRMS.Services.Document.Application.Queries.GetFolderDocuments;
using HRMS.Services.Document.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Document.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FoldersController : ControllerBase
{
    private readonly IMediator _mediator;

    public FoldersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<DocumentFolderDto>), 200)]
    public async Task<IActionResult> GetFolders(
        [FromQuery] Guid? parentFolderId = null,
        [FromQuery] string? tenantId = null)
    {
        var query = new GetFoldersQuery
        {
            ParentFolderId = parentFolderId,
            TenantId = tenantId
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(DocumentFolderDto), 200)]
    public async Task<IActionResult> GetFolder(Guid id)
    {
        var result = await _mediator.Send(new GetFolderQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateFolder([FromBody] CreateFolderCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetFolder), new { id }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateFolder(Guid id, [FromBody] UpdateFolderCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteFolder(Guid id)
    {
        await _mediator.Send(new DeleteFolderCommand { Id = id });
        return NoContent();
    }

    [HttpGet("{id:guid}/documents")]
    [ProducesResponseType(typeof(PagedResult<DocumentDto>), 200)]
    public async Task<IActionResult> GetFolderDocuments(
        Guid id,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Domain.Enums.DocumentStatus? status = null)
    {
        var query = new GetFolderDocumentsQuery
        {
            FolderId = id,
            PageNumber = pageNumber,
            PageSize = pageSize,
            Status = status
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
