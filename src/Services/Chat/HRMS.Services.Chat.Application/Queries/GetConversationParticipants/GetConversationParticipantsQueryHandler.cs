using AutoMapper;
using HRMS.Services.Chat.Application.DTOs;
using HRMS.Services.Chat.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Chat.Application.Queries.GetConversationParticipants;

public class GetConversationParticipantsQueryHandler : IRequestHandler<GetConversationParticipantsQuery, List<ConversationParticipantDto>>
{
    private readonly IChatDbContext _context;
    private readonly IMapper _mapper;

    public GetConversationParticipantsQueryHandler(IChatDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ConversationParticipantDto>> Handle(GetConversationParticipantsQuery request, CancellationToken cancellationToken)
    {
        var participants = await _context.ConversationParticipants
            .Where(p => p.ConversationId == request.ConversationId)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<ConversationParticipantDto>>(participants);
    }
}
