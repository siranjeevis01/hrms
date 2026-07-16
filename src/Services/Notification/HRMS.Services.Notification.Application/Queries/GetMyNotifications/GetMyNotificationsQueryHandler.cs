using HRMS.Services.Notification.Application.DTOs;
using HRMS.Services.Notification.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Notification.Application.Queries.GetMyNotifications;

public class GetMyNotificationsQueryHandler : IRequestHandler<GetMyNotificationsQuery, PagedResult<NotificationListDto>>
{
    private readonly INotificationDbContext _context;

    public GetMyNotificationsQueryHandler(INotificationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<NotificationListDto>> Handle(GetMyNotificationsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Notifications
            .Where(n => n.UserId == request.UserId);

        if (request.Type.HasValue)
            query = query.Where(n => n.Type == request.Type.Value);

        if (request.Category.HasValue)
            query = query.Where(n => n.Category == request.Category.Value);

        if (request.IsRead.HasValue)
            query = query.Where(n => n.IsRead == request.IsRead.Value);

        if (!string.IsNullOrEmpty(request.SearchTerm))
            query = query.Where(n => n.Title.Contains(request.SearchTerm) || n.Message.Contains(request.SearchTerm));

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(n => n.CreatedAt)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(n => new NotificationListDto
            {
                Id = n.Id,
                Title = n.Title,
                Message = n.Message,
                Type = n.Type,
                Category = n.Category,
                Priority = n.Priority,
                Status = n.Status,
                IsRead = n.IsRead,
                ActionUrl = n.ActionUrl,
                CreatedAt = n.CreatedAt
            })
            .ToListAsync(cancellationToken);

        return new PagedResult<NotificationListDto>
        {
            Items = items,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
        };
    }
}
