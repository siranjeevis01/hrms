using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Chat.Application.Commands.ArchiveChannel;

public class ArchiveChannelCommandHandler : IRequestHandler<ArchiveChannelCommand>
{
    private readonly Application.Interfaces.IChatDbContext _context;

    public ArchiveChannelCommandHandler(Application.Interfaces.IChatDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ArchiveChannelCommand request, CancellationToken cancellationToken)
    {
        var channel = await _context.ChatChannels
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (channel == null)
            throw new KeyNotFoundException($"Channel with Id {request.Id} not found.");

        channel.Archive();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
