using AutoMapper;
using HRMS.Services.Helpdesk.Application.DTOs;
using HRMS.Services.Helpdesk.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Helpdesk.Application.Queries.GetTicket;

public class GetTicketQueryHandler : IRequestHandler<GetTicketQuery, HelpdeskTicketDto?>
{
    private readonly IHelpdeskDbContext _context;
    private readonly IMapper _mapper;

    public GetTicketQueryHandler(IHelpdeskDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<HelpdeskTicketDto?> Handle(GetTicketQuery request, CancellationToken cancellationToken)
    {
        var ticket = await _context.HelpdeskTickets
            .Include(t => t.Comments)
            .Include(t => t.Attachments)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (ticket == null)
            return null;

        return _mapper.Map<HelpdeskTicketDto>(ticket);
    }
}
