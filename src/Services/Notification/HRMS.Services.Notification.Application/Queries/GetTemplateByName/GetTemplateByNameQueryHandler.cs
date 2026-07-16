using HRMS.Services.Notification.Application.DTOs;
using HRMS.Services.Notification.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Notification.Application.Queries.GetTemplateByName;

public class GetTemplateByNameQueryHandler : IRequestHandler<GetTemplateByNameQuery, NotificationTemplateDto?>
{
    private readonly INotificationDbContext _context;

    public GetTemplateByNameQueryHandler(INotificationDbContext context)
    {
        _context = context;
    }

    public async Task<NotificationTemplateDto?> Handle(GetTemplateByNameQuery request, CancellationToken cancellationToken)
    {
        var query = _context.NotificationTemplates
            .Where(t => t.Name == request.Name && t.IsActive);

        if (request.TenantId.HasValue)
            query = query.Where(t => t.TenantId == request.TenantId.Value);

        return await query
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
            .FirstOrDefaultAsync(cancellationToken);
    }
}
