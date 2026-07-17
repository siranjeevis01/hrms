using HRMS.Services.Helpdesk.Application.Commands.CreateKnowledgeArticle;
using HRMS.Services.Helpdesk.Application.Commands.PublishKnowledgeArticle;
using HRMS.Services.Helpdesk.Application.Commands.UpdateKnowledgeArticle;
using HRMS.Services.Helpdesk.Application.Queries.GetKnowledgeArticle;
using HRMS.Services.Helpdesk.Application.Queries.GetKnowledgeArticles;
using HRMS.Services.Helpdesk.Application.DTOs;
using HRMS.Services.Helpdesk.API.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Helpdesk.API.Controllers;

[ApiController]
[Route("api/helpdesk/[controller]")]
public class KnowledgeArticlesController : ControllerBase
{
    private readonly IMediator _mediator;

    public KnowledgeArticlesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<KnowledgeArticleDto>), 200)]
    public async Task<IActionResult> GetKnowledgeArticles(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Guid? categoryId = null,
        [FromQuery] bool? isPublished = null,
        [FromQuery] string? searchTerm = null,
        [FromQuery] string tenantId = "")
    {
        var query = new GetKnowledgeArticlesQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            CategoryId = categoryId,
            IsPublished = isPublished,
            SearchTerm = searchTerm,
            TenantId = tenantId
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(KnowledgeArticleDto), 200)]
    public async Task<IActionResult> GetKnowledgeArticle(Guid id)
    {
        var result = await _mediator.Send(new GetKnowledgeArticleQuery { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateKnowledgeArticle([FromBody] CreateKnowledgeArticleCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetKnowledgeArticle), new { id }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateKnowledgeArticle(Guid id, [FromBody] UpdateKnowledgeArticleCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:guid}/publish")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> PublishKnowledgeArticle(Guid id)
    {
        await _mediator.Send(new PublishKnowledgeArticleCommand { Id = id });
        return NoContent();
    }
}
