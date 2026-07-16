using AutoMapper;
using HRMS.Services.Chat.Application.DTOs;
using HRMS.Services.Chat.Application.Interfaces;
using HRMS.Shared.Kernel.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Chat.Application.Queries.GetMessages;

public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, PagedResult<MessageDto>>
{
    private readonly IChatDbContext _context;
    private readonly IMapper _mapper;

    public GetMessagesQueryHandler(IChatDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<MessageDto>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Messages
            .Include(m => m.Reactions)
            .Where(m => m.ConversationId == request.ConversationId && !m.IsDeleted)
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
