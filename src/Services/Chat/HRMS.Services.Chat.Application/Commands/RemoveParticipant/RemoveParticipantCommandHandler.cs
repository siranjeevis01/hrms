using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Chat.Application.Commands.RemoveParticipant;

public class RemoveParticipantCommandHandler : IRequestHandler<RemoveParticipantCommand>
{
    private readonly Application.Interfaces.IChatDbContext _context;

    public RemoveParticipantCommandHandler(Application.Interfaces.IChatDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RemoveParticipantCommand request, CancellationToken cancellationToken)
    {
        var conversation = await _context.Conversations
            .FirstOrDefaultAsync(c => c.Id == request.ConversationId, cancellationToken);

        if (conversation == null)
            throw new KeyNotFoundException($"Conversation with Id {request.ConversationId} not found.");

        conversation.RemoveParticipant(request.EmployeeId);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
