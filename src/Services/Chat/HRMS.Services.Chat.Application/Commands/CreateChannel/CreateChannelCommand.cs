using HRMS.Services.Chat.Domain.Enums;
using MediatR;

namespace HRMS.Services.Chat.Application.Commands.CreateChannel;

public class CreateChannelCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ChannelType Type { get; set; }
    public Guid CreatorId { get; set; }
    public Guid TenantId { get; set; }
}
