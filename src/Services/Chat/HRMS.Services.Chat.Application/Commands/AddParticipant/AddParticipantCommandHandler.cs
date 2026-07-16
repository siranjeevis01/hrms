using HRMS.Services.Chat.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Chat.Application.Commands.AddParticipant;

public class AddParticipantCommandHandler : IRequestHandler<AddParticipantCommand>
{
    private readonly Application.Interfaces.IChatDbContext _context;

    public AddParticipantCommandHandler(Application.Interfaces.IChatDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AddParticipantCommand request, CancellationToken cancellationToken)
    {
        var conversation = await _context.Conversations
            .FirstOrDefaultAsync(c => c.Id == request.ConversationId, cancellationToken);

        if (conversation == null)
            throw new KeyNotFoundException($"Conversation with Id {request.ConversationId} not found.");

        var role = Enum.TryParse<ParticipantRole>(request.Role, true, out var parsedRole)
            ? parsedRole
            : ParticipantRole.Member;

        var participant = Domain.Entities.ConversationParticipant.Create(
            request.ConversationId,
            request.EmployeeId,
            role,
            request.TenantId);

        conversation.AddParticipant(participant);
        _context.ConversationParticipants.Add(participant);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
