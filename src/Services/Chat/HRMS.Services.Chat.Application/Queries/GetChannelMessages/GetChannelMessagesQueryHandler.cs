using AutoMapper;
using HRMS.Services.Chat.Application.DTOs;
using HRMS.Services.Chat.Application.Interfaces;
using HRMS.Shared.Kernel.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Chat.Application.Queries.GetChannelMessages;

public class GetChannelMessagesQueryHandler : IRequestHandler<GetChannelMessagesQuery, PagedResult<MessageDto>>
{
    private readonly IChatDbContext _context;
    private readonly IMapper _mapper;

    public GetChannelMessagesQueryHandler(IChatDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<MessageDto>> Handle(GetChannelMessagesQuery request, CancellationToken cancellationToken)
    {
        var channel = await _context.ChatChannels
            .FirstOrDefaultAsync(c => c.Id == request.ChannelId, cancellationToken);

        if (channel == null)
            throw new KeyNotFoundException($"Channel with Id {request.ChannelId} not found.");

        var conversation = await _context.Conversations
            .FirstOrDefaultAsync(c => c.Name == channel.Name && c.Type == Domain.Enums.ConversationType.Channel, cancellationToken);

        if (conversation == null)
            return PagedResult<MessageDto>.Create(new List<MessageDto>(), 0, request.PageNumber, request.PageSize);

        var query = _context.Messages
            .Include(m => m.Reactions)
            .Where(m => m.ConversationId == conversation.Id && !m.IsDeleted)
            .OrderByDescending(m => m.CreatedAt);

        var totalCount = await query.CountAsync(cancellationToken);

        var messages = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var dtos = _mapper.Map<List<MessageDto>>(messages);

        return PagedResult<MessageDto>.Create(dtos, totalCount, request.PageNumber, request.PageSize);
    }
}
