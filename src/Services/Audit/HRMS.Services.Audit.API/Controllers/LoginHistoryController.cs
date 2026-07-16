using HRMS.Services.Audit.Application.Commands.LogLogin;
using HRMS.Services.Audit.Application.DTOs;
using HRMS.Services.Audit.Application.Queries.GetAuditLogs;
using HRMS.Services.Audit.Application.Queries.GetLoginHistory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Audit.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginHistoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public LoginHistoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedAuditResult<LoginHistoryDto>), 200)]
    public async Task<IActionResult> GetLoginHistory(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Guid? userId = null,
        [FromQuery] DateTime? fromDate = null,
        [FromQuery] DateTime? toDate = null,
        [FromQuery] bool? isSuccessful = null,
        [FromQuery] string tenantId = "")
    {
        var query = new GetLoginHistoryQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            UserId = userId,
            FromDate = fromDate,
            ToDate = toDate,
            IsSuccessful = isSuccessful,
            TenantId = tenantId
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> LogLogin([FromBody] LogLoginCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetLoginHistory), new { tenantId = command.TenantId }, id);
    }
}
