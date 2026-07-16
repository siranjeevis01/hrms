using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Chat.Application.Commands.UpdatePresence;

public class UpdatePresenceCommandHandler : IRequestHandler<UpdatePresenceCommand>
{
    private readonly Application.Interfaces.IChatDbContext _context;

    public UpdatePresenceCommandHandler(Application.Interfaces.IChatDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdatePresenceCommand request, CancellationToken cancellationToken)
    {
        var presence = await _context.UserPresences
            .FirstOrDefaultAsync(p => p.EmployeeId == request.EmployeeId, cancellationToken);

        if (presence == null)
        {
            presence = Domain.Entities.UserPresence.Create(
                request.EmployeeId,
                request.Status,
                request.TenantId);
            _context.UserPresences.Add(presence);
        }
        else
        {
            presence.UpdateStatus(request.Status);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}
