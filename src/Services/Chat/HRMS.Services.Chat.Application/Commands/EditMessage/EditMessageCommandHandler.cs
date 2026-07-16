using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Chat.Application.Commands.EditMessage;

public class EditMessageCommandHandler : IRequestHandler<EditMessageCommand>
{
    private readonly Application.Interfaces.IChatDbContext _context;

    public EditMessageCommandHandler(Application.Interfaces.IChatDbContext context)
    {
        _context = context;
    }

    public async Task Handle(EditMessageCommand request, CancellationToken cancellationToken)
    {
        var message = await _context.Messages
            .FirstOrDefaultAsync(m => m.Id == request.MessageId, cancellationToken);

        if (message == null)
            throw new KeyNotFoundException($"Message with Id {request.MessageId} not found.");

        message.UpdateContent(request.Content);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
