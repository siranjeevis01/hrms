using HRMS.Services.Notification.Application.DTOs;
using HRMS.Services.Notification.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Notification.Application.Queries.GetPreferences;

public class GetPreferencesQueryHandler : IRequestHandler<GetPreferencesQuery, List<NotificationPreferenceDto>>
{
    private readonly INotificationDbContext _context;

    public GetPreferencesQueryHandler(INotificationDbContext context)
    {
        _context = context;
    }

    public async Task<List<NotificationPreferenceDto>> Handle(GetPreferencesQuery request, CancellationToken cancellationToken)
    {
        return await _context.NotificationPreferences
            .Where(p => p.UserId == request.UserId)
            .Select(p => new NotificationPreferenceDto
            {
                Id = p.Id,
                UserId = p.UserId,
                Category = p.Category,
                Channel = p.Channel,
                IsEnabled = p.IsEnabled,
                Frequency = p.Frequency,
                QuietHoursStart = p.QuietHoursStart,
                QuietHoursEnd = p.QuietHoursEnd
            })
            .ToListAsync(cancellationToken);
    }
}
