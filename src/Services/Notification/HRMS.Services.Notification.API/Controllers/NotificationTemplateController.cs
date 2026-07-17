using HRMS.Services.Notification.Application.Commands.SendBulkNotification;
using HRMS.Services.Notification.Application.Commands.SendDepartmentNotification;
using HRMS.Services.Notification.Application.Commands.SendEmail;
using HRMS.Services.Notification.Application.Commands.SendNotification;
using HRMS.Services.Notification.Application.Commands.SendNotificationToGroup;
using HRMS.Services.Notification.Application.Commands.SendPushNotification;
using HRMS.Services.Notification.Application.Commands.SendSms;
using HRMS.Services.Notification.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Services.Notification.API.Controllers;

[ApiController]
[Route("api/notifications/[controller]")]
[Authorize]
public class NotificationSendController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationSendController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendNotification([FromBody] SendNotificationRequestDto request)
    {
        var command = new SendNotificationCommand
        {
            UserId = request.UserId,
            Title = request.Title,
            Message = request.Message,
            Type = request.Type,
            Category = request.Category,
            Priority = request.Priority,
            Channel = request.Channel,
            Data = request.Data,
            ActionUrl = request.ActionUrl,
            TemplateName = request.TemplateName,
            TemplateVariables = request.TemplateVariables
        };
        var id = await _mediator.Send(command);
        return Ok(new { id });
    }

    [HttpPost("send-bulk")]
    public async Task<IActionResult> SendBulkNotification([FromBody] SendBulkNotificationCommand command)
    {
        var count = await _mediator.Send(command);
        return Ok(new { sent = count });
    }

    [HttpPost("send-email")]
    public async Task<IActionResult> SendEmail([FromBody] EmailDto request)
    {
        var command = new SendEmailCommand
        {
            To = request.To,
            Cc = request.Cc,
            Bcc = request.Bcc,
            Subject = request.Subject,
            Body = request.Body,
            IsHtml = request.IsHtml,
            Priority = request.Priority,
            Attachments = request.Attachments,
            ScheduledAt = request.ScheduledAt
        };
        var id = await _mediator.Send(command);
        return Ok(new { id });
    }

    [HttpPost("send-sms")]
    public async Task<IActionResult> SendSms([FromBody] SmsDto request)
    {
        var command = new SendSmsCommand
        {
            PhoneNumber = request.PhoneNumber,
            Message = request.Message,
            Provider = request.Provider
        };
        var id = await _mediator.Send(command);
        return Ok(new { id });
    }

    [HttpPost("send-push")]
    public async Task<IActionResult> SendPush([FromBody] SendPushNotificationCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { id });
    }

    [HttpPost("send-to-group")]
    public async Task<IActionResult> SendToGroup([FromBody] SendNotificationToGroupCommand command)
    {
        var count = await _mediator.Send(command);
        return Ok(new { sent = count });
    }

    [HttpPost("send-to-department")]
    public async Task<IActionResult> SendToDepartment([FromBody] SendDepartmentNotificationCommand command)
    {
        var count = await _mediator.Send(command);
        return Ok(new { sent = count });
    }
}
