using MediatR;

namespace HRMS.Services.Chat.Application.Commands.ArchiveChannel;

public class ArchiveChannelCommand : IRequest
{
    public Guid Id { get; set; }
}
