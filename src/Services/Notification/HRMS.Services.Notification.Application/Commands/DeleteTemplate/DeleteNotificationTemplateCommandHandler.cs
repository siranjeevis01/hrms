using HRMS.Services.Notification.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Notification.Application.Commands.DeleteTemplate;

public class DeleteNotificationTemplateCommandHandler : IRequestHandler<DeleteNotificationTemplateCommand, Unit>
{
    private readonly INotificationDbContext _context;

    public DeleteNotificationTemplateCommandHandler(INotificationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteNotificationTemplateCommand request, CancellationToken cancellationToken)
    {
        var template = await _context.NotificationTemplates
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (template == null)
            throw new InvalidOperationException("Template not found.");

        _context.NotificationTemplates.Remove(template);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
