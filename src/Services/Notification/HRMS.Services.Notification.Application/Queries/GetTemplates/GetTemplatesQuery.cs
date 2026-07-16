using HRMS.Services.Notification.Application.DTOs;
using MediatR;

namespace HRMS.Services.Notification.Application.Queries.GetTemplates;

public record GetTemplatesQuery : IRequest<List<NotificationTemplateDto>>
{
    public Guid? TenantId { get; init; }
    public string? Category { get; init; }
    public string? Channel { get; init; }
    public bool? IsActive { get; init; }
}
