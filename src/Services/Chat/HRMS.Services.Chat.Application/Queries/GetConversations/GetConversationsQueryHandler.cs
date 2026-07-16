using AutoMapper;
using HRMS.Services.Chat.Application.DTOs;
using HRMS.Services.Chat.Application.Interfaces;
using HRMS.Shared.Kernel.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Chat.Application.Queries.GetConversations;

public class GetConversationsQueryHandler : IRequestHandler<GetConversationsQuery, PagedResult<ConversationDto>>
{
    private readonly IChatDbContext _context;
    private readonly IMapper _mapper;

    public GetConversationsQueryHandler(IChatDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<ConversationDto>> Handle(GetConversationsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Conversations
            .Include(c => c.Participants)
            .Where(c => c.Participants.Any(p => p.EmployeeId == request.EmployeeId && p.LeftAt == null))
            .OrderByDescending(c => c.LastMessageAt ?? c.CreatedAt);

        var totalCount = await query.CountAsync(cancellationToken);

        var conversations = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var dtos = _mapper.Map<List<ConversationDto>>(conversations);

        return PagedResult<ConversationDto>.Create(dtos, totalCount, request.PageNumber, request.PageSize);
    }
}
