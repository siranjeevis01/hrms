using HRMS.Services.Notification.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Notification.Application.Commands.CreateTemplate;

public class CreateNotificationTemplateCommandHandler : IRequestHandler<CreateNotificationTemplateCommand, Guid>
{
    private readonly INotificationDbContext _context;

    public CreateNotificationTemplateCommandHandler(INotificationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateNotificationTemplateCommand request, CancellationToken cancellationToken)
    {
        var template = Domain.Entities.NotificationTemplate.Create(
            request.Name, request.Category, request.Channel, request.Body,
            request.Subject, request.Variables, request.Language, request.TenantId);

        _context.NotificationTemplates.Add(template);
        await _context.SaveChangesAsync(cancellationToken);

        return template.Id;
    }
}
