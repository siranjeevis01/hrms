using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Chat.Application.Commands.RemoveReaction;

public class RemoveReactionCommandHandler : IRequestHandler<RemoveReactionCommand>
{
    private readonly Application.Interfaces.IChatDbContext _context;

    public RemoveReactionCommandHandler(Application.Interfaces.IChatDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RemoveReactionCommand request, CancellationToken cancellationToken)
    {
        var message = await _context.Messages
            .FirstOrDefaultAsync(m => m.Id == request.MessageId, cancellationToken);

        if (message == null)
            throw new KeyNotFoundException($"Message with Id {request.MessageId} not found.");

        message.RemoveReaction(request.EmployeeId, request.Emoji);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
