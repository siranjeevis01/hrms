using AutoMapper;
using HRMS.Services.Helpdesk.Application.DTOs;
using HRMS.Services.Helpdesk.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Helpdesk.Application.Queries.GetEmployeeTickets;

public class GetEmployeeTicketsQueryHandler : IRequestHandler<GetEmployeeTicketsQuery, PagedResult<HelpdeskTicketDto>>
{
    private readonly IHelpdeskDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeeTicketsQueryHandler(IHelpdeskDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<HelpdeskTicketDto>> Handle(GetEmployeeTicketsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.HelpdeskTickets
            .Where(t => t.EmployeeId == request.EmployeeId);

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
