using AutoMapper;
using HRMS.Services.Chat.Application.DTOs;
using HRMS.Services.Chat.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Chat.Application.Queries.GetMessage;

public class GetMessageQueryHandler : IRequestHandler<GetMessageQuery, MessageDto?>
{
    private readonly IChatDbContext _context;
    private readonly IMapper _mapper;

    public GetMessageQueryHandler(IChatDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<MessageDto?> Handle(GetMessageQuery request, CancellationToken cancellationToken)
    {
        var message = await _context.Messages
            .Include(m => m.Reactions)
            .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

        if (message == null)
            return null;

        return _mapper.Map<MessageDto>(message);
    }
}
