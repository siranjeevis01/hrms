using HRMS.Services.Helpdesk.Application.DTOs;
using HRMS.Services.Helpdesk.Application.Interfaces;
using HRMS.Services.Helpdesk.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Helpdesk.Application.Queries.GetHelpdeskStats;

public class GetHelpdeskStatsQueryHandler : IRequestHandler<GetHelpdeskStatsQuery, HelpdeskStatsDto>
{
    private readonly IHelpdeskDbContext _context;

    public GetHelpdeskStatsQueryHandler(IHelpdeskDbContext context)
    {
        _context = context;
    }

    public async Task<HelpdeskStatsDto> Handle(GetHelpdeskStatsQuery request, CancellationToken cancellationToken)
    {
        var tickets = await _context.HelpdeskTickets
            .Where(t => t.TenantId == request.TenantId)
            .ToListAsync(cancellationToken);

        return new HelpdeskStatsDto
        {
            TotalTickets = tickets.Count,
            OpenTickets = tickets.Count(t => t.Status == TicketStatus.Open),
            InProgressTickets = tickets.Count(t => t.Status == TicketStatus.InProgress),
            ResolvedTickets = tickets.Count(t => t.Status == TicketStatus.Resolved),
            ClosedTickets = tickets.Count(t => t.Status == TicketStatus.Closed),
            UrgentTickets = tickets.Count(t => t.Priority == TicketPriority.Urgent),
            HighPriorityTickets = tickets.Count(t => t.Priority == TicketPriority.High),
            UnassignedTickets = tickets.Count(t => t.AssignedTo == null),
            TotalArticles = await _context.KnowledgeArticles.CountAsync(a => a.TenantId == request.TenantId, cancellationToken),
            PublishedArticles = await _context.KnowledgeArticles.CountAsync(a => a.TenantId == request.TenantId && a.IsPublished, cancellationToken),
            TotalFaqs = await _context.Faqs.CountAsync(f => f.TenantId == request.TenantId, cancellationToken),
            TicketsByCategory = tickets
                .GroupBy(t => t.Category)
                .ToDictionary(g => g.Key, g => g.Count()),
            TicketsByPriority = tickets
                .GroupBy(t => t.Priority)
                .ToDictionary(g => g.Key, g => g.Count())
        };
    }
}
