using HRMS.Services.Notification.Application.Commands.CreateTemplate;
using HRMS.Services.Notification.Application.Commands.DeleteTemplate;
using HRMS.Services.Notification.Application.Commands.UpdateTemplate;
using HRMS.Services.Notification.Application.Queries.GetTemplates;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Notification.API.Controllers;

[ApiController]
[Route("api/notifications/[controller]")]
[Authorize]
public class NotificationTemplateController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationTemplateController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetTemplates(
        [FromQuery] string? category = null,
        [FromQuery] string? channel = null,
        [FromQuery] bool? isActive = null)
    {
        var result = await _mediator.Send(new GetTemplatesQuery
        {
            Category = category,
            Channel = channel,
            IsActive = isActive
        });
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateNotificationTemplateCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTemplates), new { id }, new { id });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateNotificationTemplateCommand command)
    {
        if (id != command.Id) return BadRequest("ID mismatch");
        await _mediator.Send(command);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteNotificationTemplateCommand { Id = id });
        return Ok();
    }
}
