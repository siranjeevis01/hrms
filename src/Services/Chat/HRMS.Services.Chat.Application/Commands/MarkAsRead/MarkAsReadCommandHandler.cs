using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Chat.Application.Commands.MarkAsRead;

public class MarkAsReadCommandHandler : IRequestHandler<MarkAsReadCommand>
{
    private readonly Application.Interfaces.IChatDbContext _context;

    public MarkAsReadCommandHandler(Application.Interfaces.IChatDbContext context)
    {
        _context = context;
    }

    public async Task Handle(MarkAsReadCommand request, CancellationToken cancellationToken)
    {
        var participant = await _context.ConversationParticipants
            .FirstOrDefaultAsync(p => p.ConversationId == request.ConversationId
                && p.EmployeeId == request.EmployeeId, cancellationToken);

        if (participant == null)
            throw new KeyNotFoundException($"Participant not found in conversation {request.ConversationId}.");

        participant.UpdateLastReadMessage(request.MessageId);

        var existingRead = await _context.MessageReads
            .FirstOrDefaultAsync(r => r.MessageId == request.MessageId
                && r.EmployeeId == request.EmployeeId, cancellationToken);

        if (existingRead == null)
        {
            var read = Domain.Entities.MessageRead.Create(
                request.MessageId,
                request.EmployeeId,
                participant.TenantId);
            _context.MessageReads.Add(read);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}
