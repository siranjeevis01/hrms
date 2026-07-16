using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Chat.Application.Commands.SendMessage;

public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, Guid>
{
    private readonly Application.Interfaces.IChatDbContext _context;

    public SendMessageCommandHandler(Application.Interfaces.IChatDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var message = Domain.Entities.Message.Create(
            request.ConversationId,
            request.SenderId,
            request.Content,
            request.Type,
            request.ParentMessageId,
            request.TenantId);

        _context.Messages.Add(message);

        var conversation = await _context.Conversations
            .FirstOrDefaultAsync(c => c.Id == request.ConversationId, cancellationToken);

        conversation?.UpdateLastMessageAt();

        await _context.SaveChangesAsync(cancellationToken);

        return message.Id;
    }
}
