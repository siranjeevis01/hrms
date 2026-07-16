using HRMS.Services.Notification.Application.DTOs;
using MediatR;

namespace HRMS.Services.Notification.Application.Queries.GetTemplateByName;

public record GetTemplateByNameQuery : IRequest<NotificationTemplateDto?>
{
    public string Name { get; init; } = string.Empty;
    public Guid? TenantId { get; init; }
}
