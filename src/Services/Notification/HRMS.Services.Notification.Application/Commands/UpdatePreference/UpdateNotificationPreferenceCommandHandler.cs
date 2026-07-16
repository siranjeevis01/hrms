using HRMS.Services.Notification.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Notification.Application.Commands.UpdatePreference;

public class UpdateNotificationPreferenceCommandHandler : IRequestHandler<UpdateNotificationPreferenceCommand, Unit>
{
    private readonly INotificationDbContext _context;

    public UpdateNotificationPreferenceCommandHandler(INotificationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateNotificationPreferenceCommand request, CancellationToken cancellationToken)
    {
        var preference = await _context.NotificationPreferences
            .FirstOrDefaultAsync(p => p.UserId == request.UserId
                && p.Category == request.Category && p.Channel == request.Channel, cancellationToken);

        if (preference != null)
        {
            preference.Update(request.IsEnabled, request.Frequency, request.QuietHoursStart, request.QuietHoursEnd);
        }
        else
        {
            preference = Domain.Entities.NotificationPreference.Create(
                request.UserId, request.Category, request.Channel,
                request.IsEnabled, request.Frequency, request.QuietHoursStart,
                request.QuietHoursEnd, request.TenantId);

            _context.NotificationPreferences.Add(preference);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
