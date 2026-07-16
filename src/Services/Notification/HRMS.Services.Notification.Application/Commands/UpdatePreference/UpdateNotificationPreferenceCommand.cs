using HRMS.Services.Notification.Domain.Enums;
using MediatR;

namespace HRMS.Services.Notification.Application.Commands.UpdatePreference;

public record UpdateNotificationPreferenceCommand : IRequest<Unit>
{
    public Guid UserId { get; init; }
    public NotificationCategory Category { get; init; }
    public NotificationChannel Channel { get; init; }
    public bool IsEnabled { get; init; }
    public NotificationFrequency Frequency { get; init; } = NotificationFrequency.Immediate;
    public TimeOnly? QuietHoursStart { get; init; }
    public TimeOnly? QuietHoursEnd { get; init; }
    public Guid? TenantId { get; init; }
}
