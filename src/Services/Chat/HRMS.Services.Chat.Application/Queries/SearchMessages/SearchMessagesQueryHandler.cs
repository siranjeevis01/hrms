using AutoMapper;
using HRMS.Services.Chat.Application.DTOs;
using HRMS.Services.Chat.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Chat.Application.Queries.SearchMessages;

public class SearchMessagesQueryHandler : IRequestHandler<SearchMessagesQuery, List<MessageDto>>
{
    private readonly IChatDbContext _context;
    private readonly IMapper _mapper;

    public SearchMessagesQueryHandler(IChatDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<MessageDto>> Handle(SearchMessagesQuery request, CancellationToken cancellationToken)
    {
        var messages = await _context.Messages
            .Include(m => m.Reactions)
            .Where(m => m.ConversationId == request.ConversationId
                && !m.IsDeleted
                && m.Content.Contains(request.SearchTerm))
            .OrderByDescending(m => m.CreatedAt)
            .Take(request.MaxResults)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<MessageDto>>(messages);
    }
}
