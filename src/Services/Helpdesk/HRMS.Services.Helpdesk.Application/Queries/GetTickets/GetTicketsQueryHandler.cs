using AutoMapper;
using HRMS.Services.Helpdesk.Application.DTOs;
using HRMS.Services.Helpdesk.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Helpdesk.Application.Queries.GetTickets;

public class GetTicketsQueryHandler : IRequestHandler<GetTicketsQuery, PagedResult<HelpdeskTicketDto>>
{
    private readonly IHelpdeskDbContext _context;
    private readonly IMapper _mapper;

    public GetTicketsQueryHandler(IHelpdeskDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<HelpdeskTicketDto>> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.HelpdeskTickets.AsQueryable();

        if (request.EmployeeId.HasValue)
            query = query.Where(t => t.EmployeeId == request.EmployeeId.Value);

        if (request.Status.HasValue)
            query = query.Where(t => t.Status == request.Status.Value);

        if (request.Priority.HasValue)
            query = query.Where(t => t.Priority == request.Priority.Value);

        if (request.Category.HasValue)
            query = query.Where(t => t.Category == request.Category.Value);

        if (request.AssignedTo.HasValue)
            query = query.Where(t => t.AssignedTo == request.AssignedTo.Value);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var search = request.SearchTerm.ToLower();
            query = query.Where(t =>
                t.Subject.ToLower().Contains(search) ||
                t.Description.ToLower().Contains(search));
        }

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
