using AutoMapper;
using HRMS.Services.Helpdesk.Application.DTOs;
using HRMS.Services.Helpdesk.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Helpdesk.Application.Queries.GetAssignedTickets;

public class GetAssignedTicketsQueryHandler : IRequestHandler<GetAssignedTicketsQuery, PagedResult<HelpdeskTicketDto>>
{
    private readonly IHelpdeskDbContext _context;
    private readonly IMapper _mapper;

    public GetAssignedTicketsQueryHandler(IHelpdeskDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<HelpdeskTicketDto>> Handle(GetAssignedTicketsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.HelpdeskTickets
            .Where(t => t.AssignedTo == request.AssignedTo);

        if (request.Status.HasValue)
            query = query.Where(t => t.Status == request.Status.Value);

        var totalCount = await query.CountAsync(cancellationToken);

        var tickets = await query
            .OrderByDescending(t => t.CreatedAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var items = _mapper.Map<List<HelpdeskTicketDto>>(tickets);

        return new PagedResult<HelpdeskTicketDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}
