using HRMS.Services.Report.Application.DTOs;
using HRMS.Services.Report.Application.Queries.GetReportInstance;
using HRMS.Services.Report.Application.Queries.GetReportInstances;
using HRMS.Services.Report.Application.Queries.GetReportTemplates;
using HRMS.Services.Report.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Report.API.Controllers;

[ApiController]
[Route("api/reports/[controller]")]
public class ReportInstancesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReportInstancesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedReportResult<ReportInstanceDto>), 200)]
    public async Task<IActionResult> GetReportInstances(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Guid? templateId = null,
        [FromQuery] ReportStatus? status = null,
        [FromQuery] string tenantId = "")
    {
        var query = new GetReportInstancesQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TemplateId = templateId,
            Status = status,
            TenantId = tenantId
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ReportInstanceDto), 200)]
    public async Task<IActionResult> GetReportInstance(Guid id, [FromQuery] string tenantId = "")
    {
        var result = await _mediator.Send(new GetReportInstanceQuery { Id = id, TenantId = tenantId });
        if (result == null) return NotFound();
        return Ok(result);
    }
}
