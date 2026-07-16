using HRMS.Services.Notification.Application.DTOs;
using HRMS.Services.Notification.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Notification.Application.Queries.GetTemplates;

public class GetTemplatesQueryHandler : IRequestHandler<GetTemplatesQuery, List<NotificationTemplateDto>>
{
    private readonly INotificationDbContext _context;

    public GetTemplatesQueryHandler(INotificationDbContext context)
    {
        _context = context;
    }

    public async Task<List<NotificationTemplateDto>> Handle(GetTemplatesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.NotificationTemplates.AsQueryable();

        if (request.TenantId.HasValue)
            query = query.Where(t => t.TenantId == request.TenantId.Value);

        if (!string.IsNullOrEmpty(request.Category) && Enum.TryParse<Domain.Enums.NotificationCategory>(request.Category, true, out var cat))
            query = query.Where(t => t.Category == cat);

        if (!string.IsNullOrEmpty(request.Channel) && Enum.TryParse<Domain.Enums.NotificationChannel>(request.Channel, true, out var ch))
            query = query.Where(t => t.Channel == ch);

        if (request.IsActive.HasValue)
            query = query.Where(t => t.IsActive == request.IsActive.Value);

        return await query
            .OrderBy(t => t.Name)
            .Select(t => new NotificationTemplateDto
            {
                Id = t.Id,
                Name = t.Name,
                Category = t.Category,
                Channel = t.Channel,
                Subject = t.Subject,
                Body = t.Body,
                Variables = t.Variables,
                IsActive = t.IsActive,
                Language = t.Language,
                CreatedAt = t.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
