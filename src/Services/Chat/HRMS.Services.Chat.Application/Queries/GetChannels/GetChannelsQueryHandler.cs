using AutoMapper;
using HRMS.Services.Chat.Application.DTOs;
using HRMS.Services.Chat.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Chat.Application.Queries.GetChannels;

public class GetChannelsQueryHandler : IRequestHandler<GetChannelsQuery, List<ChatChannelDto>>
{
    private readonly IChatDbContext _context;
    private readonly IMapper _mapper;

    public GetChannelsQueryHandler(IChatDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ChatChannelDto>> Handle(GetChannelsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.ChatChannels
            .Where(c => c.TenantId == request.TenantId);

        if (!request.IncludeArchived)
            query = query.Where(c => !c.IsArchived);

        var channels = await query.ToListAsync(cancellationToken);

        return _mapper.Map<List<ChatChannelDto>>(channels);
    }
}
