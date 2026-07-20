using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HRMS.Services.Leave.Application.Events;

public class LeaveRejectedEventHandler : INotificationHandler<LeaveRejectedEvent>
{
    private readonly INotificationService _notificationService;
    private readonly ILogger<LeaveRejectedEventHandler> _logger;

    public LeaveRejectedEventHandler(INotificationService notificationService, ILogger<LeaveRejectedEventHandler> logger)
    {
        _notificationService = notificationService;
        _logger = logger;
    }

    public async Task Handle(LeaveRejectedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Processing LeaveRejectedEvent for Employee {EmployeeId}, Leave {LeaveApplicationId}",
            notification.EmployeeId, notification.LeaveApplicationId);

        var subject = "Your Leave Request Has Been Rejected";
        var body = $"""
            <div style="font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px;">
                <h2 style="color: #dc2626;">Leave Rejected</h2>
                <p>Your leave request has been rejected.</p>
                <table style="width: 100%; border-collapse: collapse; margin: 20px 0;">
                    <tr><td style="padding: 8px; font-weight: bold; border-bottom: 1px solid #eee;">Rejection Reason</td><td style="padding: 8px; border-bottom: 1px solid #eee;">{notification.RejectionReason ?? "No reason provided"}</td></tr>
                    <tr><td style="padding: 8px; font-weight: bold; border-bottom: 1px solid #eee;">Rejected On</td><td style="padding: 8px; border-bottom: 1px solid #eee;">{notification.RejectedAt:dd MMM yyyy HH:mm} UTC</td></tr>
                </table>
                <p style="color: #666; font-size: 14px;">If you believe this is an error, please contact your manager or HR department.</p>
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
