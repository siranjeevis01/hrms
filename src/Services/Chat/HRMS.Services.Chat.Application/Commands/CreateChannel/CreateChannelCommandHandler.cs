using MediatR;

namespace HRMS.Services.Chat.Application.Commands.CreateChannel;

public class CreateChannelCommandHandler : IRequestHandler<CreateChannelCommand, Guid>
{
    private readonly Application.Interfaces.IChatDbContext _context;

    public CreateChannelCommandHandler(Application.Interfaces.IChatDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateChannelCommand request, CancellationToken cancellationToken)
    {
        var channel = Domain.Entities.ChatChannel.Create(
            request.Name,
            request.Type,
            request.CreatorId,
            request.Description,
            request.TenantId);

        _context.ChatChannels.Add(channel);
        await _context.SaveChangesAsync(cancellationToken);

        return channel.Id;
    }
}
