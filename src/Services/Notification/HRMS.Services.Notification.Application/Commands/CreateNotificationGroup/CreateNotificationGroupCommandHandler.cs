using HRMS.Services.Notification.Application.Interfaces;
using MediatR;
using System.Text.Json;

namespace HRMS.Services.Notification.Application.Commands.CreateNotificationGroup;

public class CreateNotificationGroupCommandHandler : IRequestHandler<CreateNotificationGroupCommand, Guid>
{
    private readonly INotificationDbContext _context;

    public CreateNotificationGroupCommandHandler(INotificationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateNotificationGroupCommand request, CancellationToken cancellationToken)
    {
        var members = request.MemberIds != null
            ? JsonSerializer.Serialize(request.MemberIds)
            : null;

        var group = Domain.Entities.NotificationGroup.Create(
            request.Name, request.Description, members, request.TenantId);

        _context.NotificationGroups.Add(group);
        await _context.SaveChangesAsync(cancellationToken);

        return group.Id;
    }
}
