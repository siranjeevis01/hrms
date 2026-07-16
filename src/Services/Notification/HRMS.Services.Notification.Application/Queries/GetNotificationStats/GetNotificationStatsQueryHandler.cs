using HRMS.Services.Notification.Application.DTOs;
using HRMS.Services.Notification.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Notification.Application.Queries.GetNotificationStats;

public class GetNotificationStatsQueryHandler : IRequestHandler<GetNotificationStatsQuery, NotificationStatsDto>
{
    private readonly INotificationDbContext _context;

    public GetNotificationStatsQueryHandler(INotificationDbContext context)
    {
        _context = context;
    }

    public async Task<NotificationStatsDto> Handle(GetNotificationStatsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Notifications.AsQueryable();

        if (request.UserId.HasValue)
            query = query.Where(n => n.UserId == request.UserId.Value);

        if (request.FromDate.HasValue)
            query = query.Where(n => n.CreatedAt >= request.FromDate.Value);

        if (request.ToDate.HasValue)
            query = query.Where(n => n.CreatedAt <= request.ToDate.Value);

        var totalSent = await query.CountAsync(n => n.Status == Domain.Enums.NotificationStatus.Sent
            || n.Status == Domain.Enums.NotificationStatus.Delivered
            || n.Status == Domain.Enums.NotificationStatus.Read, cancellationToken);

        var totalDelivered = await query.CountAsync(n => n.Status == Domain.Enums.NotificationStatus.Delivered
            || n.Status == Domain.Enums.NotificationStatus.Read, cancellationToken);

        var totalRead = await query.CountAsync(n => n.IsRead, cancellationToken);

        var totalFailed = await query.CountAsync(n => n.Status == Domain.Enums.NotificationStatus.Failed, cancellationToken);

        var totalPending = await query.CountAsync(n => n.Status == Domain.Enums.NotificationStatus.Pending, cancellationToken);

        var byCategory = await query.GroupBy(n => n.Category)
            .Select(g => new CategoryStatDto { Category = g.Key, Count = g.Count() })
            .ToListAsync(cancellationToken);

        var byChannel = await query.GroupBy(n => n.Channel)
            .Select(g => new ChannelStatDto { Channel = g.Key, Count = g.Count() })
            .ToListAsync(cancellationToken);

        return new NotificationStatsDto
        {
            TotalSent = totalSent,
            TotalDelivered = totalDelivered,
            TotalRead = totalRead,
            TotalFailed = totalFailed,
            TotalPending = totalPending,
            ByCategory = byCategory,
            ByChannel = byChannel
        };
    }
}
