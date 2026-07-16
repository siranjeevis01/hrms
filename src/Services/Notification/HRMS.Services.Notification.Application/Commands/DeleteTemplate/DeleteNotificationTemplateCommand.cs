using MediatR;

namespace HRMS.Services.Notification.Application.Commands.DeleteTemplate;

public record DeleteNotificationTemplateCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
}
