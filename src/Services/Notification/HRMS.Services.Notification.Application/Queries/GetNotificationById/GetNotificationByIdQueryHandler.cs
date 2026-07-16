using HRMS.Services.Notification.Application.DTOs;
using HRMS.Services.Notification.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Notification.Application.Queries.GetNotificationById;

public class GetNotificationByIdQueryHandler : IRequestHandler<GetNotificationByIdQuery, NotificationDto?>
{
    private readonly INotificationDbContext _context;

    public GetNotificationByIdQueryHandler(INotificationDbContext context)
    {
        _context = context;
    }

    public async Task<NotificationDto?> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Notifications
            .Where(n => n.Id == request.Id && n.UserId == request.UserId)
            .Select(n => new NotificationDto
            {
                Id = n.Id,
                UserId = n.UserId,
                Title = n.Title,
                Message = n.Message,
                Type = n.Type,
                Category = n.Category,
                Priority = n.Priority,
                Status = n.Status,
                Channel = n.Channel,
                SentAt = n.SentAt,
                DeliveredAt = n.DeliveredAt,
                ReadAt = n.ReadAt,
                FailedAt = n.FailedAt,
                FailureReason = n.FailureReason,
                Data = n.Data,
                ActionUrl = n.ActionUrl,
                IsRead = n.IsRead,
                CreatedAt = n.CreatedAt
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}
