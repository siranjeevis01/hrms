using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Chat.Application.Commands.UpdateChannel;

public class UpdateChannelCommandHandler : IRequestHandler<UpdateChannelCommand>
{
    private readonly Application.Interfaces.IChatDbContext _context;

    public UpdateChannelCommandHandler(Application.Interfaces.IChatDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateChannelCommand request, CancellationToken cancellationToken)
    {
        var channel = await _context.ChatChannels
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (channel == null)
            throw new KeyNotFoundException($"Channel with Id {request.Id} not found.");

        channel.Update(request.Name, request.Description, request.Type);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
