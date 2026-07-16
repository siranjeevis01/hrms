using AutoMapper;
using HRMS.Services.Chat.Application.DTOs;
using HRMS.Services.Chat.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Chat.Application.Queries.GetMessageReactions;

public class GetMessageReactionsQueryHandler : IRequestHandler<GetMessageReactionsQuery, List<MessageReactionDto>>
{
    private readonly IChatDbContext _context;
    private readonly IMapper _mapper;

    public GetMessageReactionsQueryHandler(IChatDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<MessageReactionDto>> Handle(GetMessageReactionsQuery request, CancellationToken cancellationToken)
    {
        var reactions = await _context.MessageReactions
            .Where(r => r.MessageId == request.MessageId)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<MessageReactionDto>>(reactions);
    }
}
