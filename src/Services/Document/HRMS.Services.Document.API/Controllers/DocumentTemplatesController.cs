using HRMS.Services.Document.Application.Commands.CreateDocumentTemplate;
using HRMS.Services.Document.Application.Commands.UpdateDocumentTemplate;
using HRMS.Services.Document.Application.Commands.DeleteDocumentTemplate;
using HRMS.Services.Document.Application.Queries.GetDocumentTemplates;
using HRMS.Services.Document.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Document.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentTemplatesController : ControllerBase
{
    private readonly IMediator _mediator;

    public DocumentTemplatesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<DocumentTemplateDto>), 200)]
    public async Task<IActionResult> GetTemplates(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Domain.Enums.DocumentCategory? category = null,
        [FromQuery] string? searchTerm = null)
    {
        var query = new GetDocumentTemplatesQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            Category = category,
            SearchTerm = searchTerm
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateTemplate([FromBody] CreateDocumentTemplateCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTemplates), new { id }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateTemplate(Guid id, [FromBody] UpdateDocumentTemplateCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteTemplate(Guid id)
    {
        await _mediator.Send(new DeleteDocumentTemplateCommand { Id = id });
        return NoContent();
    }
}
