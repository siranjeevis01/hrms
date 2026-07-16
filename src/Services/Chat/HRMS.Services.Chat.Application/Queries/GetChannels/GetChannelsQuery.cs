using HRMS.Services.Chat.Application.DTOs;
using MediatR;

namespace HRMS.Services.Chat.Application.Queries.GetChannels;

public class GetChannelsQuery : IRequest<List<ChatChannelDto>>
{
    public Guid TenantId { get; set; }
    public bool IncludeArchived { get; set; } = false;
}
