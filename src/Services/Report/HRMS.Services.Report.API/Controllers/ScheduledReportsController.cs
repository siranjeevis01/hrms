using HRMS.Services.Report.Application.Commands.ScheduleReport;
using HRMS.Services.Report.Application.Commands.UpdateScheduledReport;
using HRMS.Services.Report.Application.DTOs;
using HRMS.Services.Report.Application.Queries.GetScheduledReports;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Report.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScheduledReportsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ScheduledReportsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ScheduledReportDto>), 200)]
    public async Task<IActionResult> GetScheduledReports(
        [FromQuery] Guid? templateId = null,
        [FromQuery] bool? isActive = null,
        [FromQuery] string tenantId = "")
    {
        var query = new GetScheduledReportsQuery
        {
            TemplateId = templateId,
            IsActive = isActive,
            TenantId = tenantId
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> ScheduleReport([FromBody] ScheduleReportCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetScheduledReports), new { tenantId = command.TenantId }, id);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateScheduledReport(Guid id, [FromBody] UpdateScheduledReportCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }
}
