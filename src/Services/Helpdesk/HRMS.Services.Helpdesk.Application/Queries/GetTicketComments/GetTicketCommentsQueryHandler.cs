using AutoMapper;
using HRMS.Services.Helpdesk.Application.DTOs;
using HRMS.Services.Helpdesk.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Helpdesk.Application.Queries.GetTicketComments;

public class GetTicketCommentsQueryHandler : IRequestHandler<GetTicketCommentsQuery, List<TicketCommentDto>>
{
    private readonly IHelpdeskDbContext _context;
    private readonly IMapper _mapper;

    public GetTicketCommentsQueryHandler(IHelpdeskDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TicketCommentDto>> Handle(GetTicketCommentsQuery request, CancellationToken cancellationToken)
    {
        var comments = await _context.TicketComments
            .Where(c => c.TicketId == request.TicketId)
            .OrderBy(c => c.CreatedAt)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<TicketCommentDto>>(comments);
    }
}
