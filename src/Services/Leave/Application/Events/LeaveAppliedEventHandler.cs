using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HRMS.Services.Leave.Application.Events;

public class LeaveAppliedEventHandler : INotificationHandler<LeaveAppliedEvent>
{
    private readonly INotificationService _notificationService;
    private readonly ILogger<LeaveAppliedEventHandler> _logger;

    public LeaveAppliedEventHandler(INotificationService notificationService, ILogger<LeaveAppliedEventHandler> logger)
    {
        _notificationService = notificationService;
        _logger = logger;
    }

    public async Task Handle(LeaveAppliedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Processing LeaveAppliedEvent for Employee {EmployeeId}, Leave {LeaveApplicationId}",
            notification.EmployeeId, notification.LeaveApplicationId);

        var subject = "New Leave Request Submitted";
        var body = $"""
            <div style="font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px;">
                <h2 style="color: #2563eb;">Leave Request Submitted</h2>
                <p>A new leave request has been submitted and is pending approval.</p>
                <table style="width: 100%; border-collapse: collapse; margin: 20px 0;">
                    <tr><td style="padding: 8px; font-weight: bold; border-bottom: 1px solid #eee;">Start Date</td><td style="padding: 8px; border-bottom: 1px solid #eee;">{notification.StartDate:dd MMM yyyy}</td></tr>
                    <tr><td style="padding: 8px; font-weight: bold; border-bottom: 1px solid #eee;">End Date</td><td style="padding: 8px; border-bottom: 1px solid #eee;">{notification.EndDate:dd MMM yyyy}</td></tr>
                    <tr><td style="padding: 8px; font-weight: bold; border-bottom: 1px solid #eee;">Duration</td><td style="padding: 8px; border-bottom: 1px solid #eee;">{notification.TotalDays} day(s)</td></tr>
                </table>
                <hr style="border: none; border-top: 1px solid #eee; margin: 20px 0;" />
                <p style="color: #999; font-size: 12px;">This email was sent from HRMS Pro.</p>
            </div>
            """;

        await _notificationService.SendEmailAsync(
            $"employee-{notification.EmployeeId}@hrms.local",
            subject,
            body,
            true,
            cancellationToken);
    }
}
