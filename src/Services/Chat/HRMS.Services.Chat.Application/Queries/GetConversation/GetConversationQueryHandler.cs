using AutoMapper;
using HRMS.Services.Chat.Application.DTOs;
using HRMS.Services.Chat.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Chat.Application.Queries.GetConversation;

public class GetConversationQueryHandler : IRequestHandler<GetConversationQuery, ConversationDto?>
{
    private readonly IChatDbContext _context;
    private readonly IMapper _mapper;

    public GetConversationQueryHandler(IChatDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ConversationDto?> Handle(GetConversationQuery request, CancellationToken cancellationToken)
    {
        var conversation = await _context.Conversations
            .Include(c => c.Participants)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (conversation == null)
            return null;

        return _mapper.Map<ConversationDto>(conversation);
    }
}
