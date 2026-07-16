using HRMS.Services.Notification.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Notification.Application.Queries.GetUnreadCount;

public class GetUnreadCountQueryHandler : IRequestHandler<GetUnreadCountQuery, int>
{
    private readonly INotificationDbContext _context;

    public GetUnreadCountQueryHandler(INotificationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(GetUnreadCountQuery request, CancellationToken cancellationToken)
    {
        return await _context.Notifications
            .CountAsync(n => n.UserId == request.UserId && !n.IsRead, cancellationToken);
    }
}
