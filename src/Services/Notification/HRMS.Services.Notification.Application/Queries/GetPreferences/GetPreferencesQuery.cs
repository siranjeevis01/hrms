using HRMS.Services.Notification.Application.DTOs;
using MediatR;

namespace HRMS.Services.Notification.Application.Queries.GetPreferences;

public record GetPreferencesQuery : IRequest<List<NotificationPreferenceDto>>
{
    public Guid UserId { get; init; }
}
