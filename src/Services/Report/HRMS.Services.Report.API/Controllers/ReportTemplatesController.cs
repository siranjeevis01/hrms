using HRMS.Services.Report.Application.Commands.CreateReportTemplate;
using HRMS.Services.Report.Application.Commands.GenerateReport;
using HRMS.Services.Report.Application.Commands.GrantReportAccess;
using HRMS.Services.Report.Application.Commands.UpdateReportTemplate;
using HRMS.Services.Report.Application.DTOs;
using HRMS.Services.Report.Application.Queries.GetReportCategories;
using HRMS.Services.Report.Application.Queries.GetReportTemplate;
using HRMS.Services.Report.Application.Queries.GetReportTemplates;
using HRMS.Services.Report.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Report.API.Controllers;

[ApiController]
[Route("api/reports/[controller]")]
public class ReportTemplatesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReportTemplatesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedReportResult<ReportTemplateDto>), 200)]
    public async Task<IActionResult> GetReportTemplates(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] ReportCategory? category = null,
        [FromQuery] ReportType? reportType = null,
        [FromQuery] string? searchTerm = null,
        [FromQuery] string tenantId = "")
    {
        var query = new GetReportTemplatesQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            Category = category,
            ReportType = reportType,
            SearchTerm = searchTerm,
            TenantId = tenantId
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ReportTemplateDto), 200)]
    public async Task<IActionResult> GetReportTemplate(Guid id, [FromQuery] string tenantId = "")
    {
        var result = await _mediator.Send(new GetReportTemplateQuery { Id = id, TenantId = tenantId });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("categories")]
    [ProducesResponseType(typeof(List<ReportCategoryDto>), 200)]
    public async Task<IActionResult> GetReportCategories([FromQuery] string tenantId = "")
    {
        var result = await _mediator.Send(new GetReportCategoriesQuery { TenantId = tenantId });
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateReportTemplate([FromBody] CreateReportTemplateCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetReportTemplate), new { id, tenantId = command.TenantId }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateReportTemplate(Guid id, [FromBody] UpdateReportTemplateCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("generate")]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> GenerateReport([FromBody] GenerateReportCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetReportTemplate), new { id = command.TemplateId, tenantId = command.TenantId }, id);
    }

    [HttpPost("access")]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> GrantReportAccess([FromBody] GrantReportAccessCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetReportTemplate), new { id = command.TemplateId, tenantId = command.TenantId }, id);
    }
}
