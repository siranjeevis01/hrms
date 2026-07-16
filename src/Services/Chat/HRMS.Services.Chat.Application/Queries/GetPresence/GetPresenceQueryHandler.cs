using AutoMapper;
using HRMS.Services.Chat.Application.DTOs;
using HRMS.Services.Chat.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Chat.Application.Queries.GetPresence;

public class GetPresenceQueryHandler : IRequestHandler<GetPresenceQuery, UserPresenceDto?>
{
    private readonly IChatDbContext _context;
    private readonly IMapper _mapper;

    public GetPresenceQueryHandler(IChatDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserPresenceDto?> Handle(GetPresenceQuery request, CancellationToken cancellationToken)
    {
        var presence = await _context.UserPresences
            .FirstOrDefaultAsync(p => p.EmployeeId == request.EmployeeId, cancellationToken);

        if (presence == null)
            return null;

        return _mapper.Map<UserPresenceDto>(presence);
    }
}
