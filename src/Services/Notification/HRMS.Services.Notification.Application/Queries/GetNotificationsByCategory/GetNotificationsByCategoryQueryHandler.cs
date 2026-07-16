using HRMS.Services.Notification.Application.DTOs;
using HRMS.Services.Notification.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Notification.Application.Queries.GetNotificationsByCategory;

public class GetNotificationsByCategoryQueryHandler : IRequestHandler<GetNotificationsByCategoryQuery, List<NotificationListDto>>
{
    private readonly INotificationDbContext _context;

    public GetNotificationsByCategoryQueryHandler(INotificationDbContext context)
    {
        _context = context;
    }

    public async Task<List<NotificationListDto>> Handle(GetNotificationsByCategoryQuery request, CancellationToken cancellationToken)
    {
        return await _context.Notifications
            .Where(n => n.UserId == request.UserId && n.Category == request.Category)
            .OrderByDescending(n => n.CreatedAt)
            .Take(request.Limit)
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
    }
}
