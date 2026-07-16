using MediatR;

namespace HRMS.Services.Chat.Application.Commands.CreateConversation;

public class CreateConversationCommandHandler : IRequestHandler<CreateConversationCommand, Guid>
{
    private readonly Application.Interfaces.IChatDbContext _context;

    public CreateConversationCommandHandler(Application.Interfaces.IChatDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateConversationCommand request, CancellationToken cancellationToken)
    {
        var conversation = Domain.Entities.Conversation.Create(
            request.Name,
            request.Type,
            request.CreatedBy,
            request.Description,
            request.IsPrivate,
            request.TenantId);

        var creatorParticipant = Domain.Entities.ConversationParticipant.Create(
            conversation.Id,
            request.CreatedBy,
            Domain.Enums.ParticipantRole.Admin,
            request.TenantId);

        conversation.AddParticipant(creatorParticipant);

        foreach (var participantId in request.ParticipantIds.Where(id => id != request.CreatedBy))
        {
            var participant = Domain.Entities.ConversationParticipant.Create(
                conversation.Id,
                participantId,
                Domain.Enums.ParticipantRole.Member,
                request.TenantId);
            conversation.AddParticipant(participant);
        }

        _context.Conversations.Add(conversation);
        await _context.SaveChangesAsync(cancellationToken);

        return conversation.Id;
    }
}
