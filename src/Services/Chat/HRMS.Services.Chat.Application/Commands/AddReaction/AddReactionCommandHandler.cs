using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Chat.Application.Commands.AddReaction;

public class AddReactionCommandHandler : IRequestHandler<AddReactionCommand>
{
    private readonly Application.Interfaces.IChatDbContext _context;

    public AddReactionCommandHandler(Application.Interfaces.IChatDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AddReactionCommand request, CancellationToken cancellationToken)
    {
        var message = await _context.Messages
            .FirstOrDefaultAsync(m => m.Id == request.MessageId, cancellationToken);

        if (message == null)
            throw new KeyNotFoundException($"Message with Id {request.MessageId} not found.");

        var existingReaction = await _context.MessageReactions
            .FirstOrDefaultAsync(r => r.MessageId == request.MessageId
                && r.EmployeeId == request.EmployeeId
                && r.Emoji == request.Emoji, cancellationToken);

        if (existingReaction != null)
            return;

        var reaction = Domain.Entities.MessageReaction.Create(
            request.MessageId,
            request.EmployeeId,
            request.Emoji,
            request.TenantId);

        message.AddReaction(reaction);
        _context.MessageReactions.Add(reaction);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
