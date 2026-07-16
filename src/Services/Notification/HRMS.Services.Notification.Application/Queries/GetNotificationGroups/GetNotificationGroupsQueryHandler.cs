using HRMS.Services.Notification.Application.DTOs;
using HRMS.Services.Notification.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Notification.Application.Queries.GetNotificationGroups;

public class GetNotificationGroupsQueryHandler : IRequestHandler<GetNotificationGroupsQuery, List<NotificationGroupDto>>
{
    private readonly INotificationDbContext _context;

    public GetNotificationGroupsQueryHandler(INotificationDbContext context)
    {
        _context = context;
    }

    public async Task<List<NotificationGroupDto>> Handle(GetNotificationGroupsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.NotificationGroups.AsQueryable();

        if (request.TenantId.HasValue)
            query = query.Where(g => g.TenantId == request.TenantId.Value);

        if (request.IsActive.HasValue)
            query = query.Where(g => g.IsActive == request.IsActive.Value);

        return await query
            .OrderBy(g => g.Name)
            .Select(g => new NotificationGroupDto
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description,
                Members = g.Members,
                IsActive = g.IsActive,
                CreatedAt = g.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
