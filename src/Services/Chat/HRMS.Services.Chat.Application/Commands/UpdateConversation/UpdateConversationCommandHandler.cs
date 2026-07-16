using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Chat.Application.Commands.UpdateConversation;

public class UpdateConversationCommandHandler : IRequestHandler<UpdateConversationCommand>
{
    private readonly Application.Interfaces.IChatDbContext _context;

    public UpdateConversationCommandHandler(Application.Interfaces.IChatDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateConversationCommand request, CancellationToken cancellationToken)
    {
        var conversation = await _context.Conversations
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (conversation == null)
            throw new KeyNotFoundException($"Conversation with Id {request.Id} not found.");

        conversation.Update(request.Name, request.Description, request.IsPrivate);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
