using HRMS.Services.Notification.Application.DTOs;
using HRMS.Services.Notification.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Notification.Application.Queries.GetDeliveryLogs;

public class GetDeliveryLogsQueryHandler : IRequestHandler<GetDeliveryLogsQuery, PagedResult<DeliveryLogDto>>
{
    private readonly INotificationDbContext _context;

    public GetDeliveryLogsQueryHandler(INotificationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<DeliveryLogDto>> Handle(GetDeliveryLogsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.NotificationDeliveryLogs.AsQueryable();

        if (request.NotificationId.HasValue)
            query = query.Where(l => l.NotificationId == request.NotificationId.Value);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(l => l.CreatedAt)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(l => new DeliveryLogDto
            {
                Id = l.Id,
                NotificationId = l.NotificationId,
                Channel = l.Channel,
                Provider = l.Provider,
                ProviderMessageId = l.ProviderMessageId,
                Status = l.Status,
                Response = l.Response,
                AttemptCount = l.AttemptCount,
                LastAttemptAt = l.LastAttemptAt,
                NextRetryAt = l.NextRetryAt,
                CreatedAt = l.CreatedAt
            })
            .ToListAsync(cancellationToken);

        return new PagedResult<DeliveryLogDto>
        {
            Items = items,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
        };
    }
}
