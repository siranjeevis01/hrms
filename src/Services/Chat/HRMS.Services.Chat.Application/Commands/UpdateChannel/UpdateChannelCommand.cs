using MediatR;

namespace HRMS.Services.Chat.Application.Commands.UpdateChannel;

public class UpdateChannelCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public HRMS.Services.Chat.Domain.Enums.ChannelType? Type { get; set; }
}
