using HRMS.Services.Travel.Application.DTOs;
using HRMS.Services.Travel.Application.Interfaces;
using HRMS.Services.Travel.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Travel.Application.Queries.GetTravelStats;

public class GetTravelStatsQueryHandler : IRequestHandler<GetTravelStatsQuery, TravelStatsDto>
{
    private readonly ITravelDbContext _context;

    public GetTravelStatsQueryHandler(ITravelDbContext context)
    {
        _context = context;
    }

    public async Task<TravelStatsDto> Handle(GetTravelStatsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.TravelRequests
            .Where(t => t.TenantId == request.TenantId);

        if (request.EmployeeId.HasValue)
            query = query.Where(t => t.EmployeeId == request.EmployeeId.Value);

        if (request.FromDate.HasValue)
            query = query.Where(t => t.CreatedAt >= request.FromDate.Value);

        if (request.ToDate.HasValue)
            query = query.Where(t => t.CreatedAt <= request.ToDate.Value);

        var travelRequests = await query.ToListAsync(cancellationToken);

        return new TravelStatsDto
        {
            TotalRequests = travelRequests.Count,
            DraftRequests = travelRequests.Count(t => t.Status == TravelRequestStatus.Draft),
            SubmittedRequests = travelRequests.Count(t => t.Status == TravelRequestStatus.Submitted),
            ApprovedRequests = travelRequests.Count(t => t.Status == TravelRequestStatus.Approved),
            RejectedRequests = travelRequests.Count(t => t.Status == TravelRequestStatus.Rejected),
            InProgressRequests = travelRequests.Count(t => t.Status == TravelRequestStatus.InProgress),
            CompletedRequests = travelRequests.Count(t => t.Status == TravelRequestStatus.Completed),
            CancelledRequests = travelRequests.Count(t => t.Status == TravelRequestStatus.Cancelled),
            TotalEstimatedCost = travelRequests.Sum(t => t.EstimatedCost),
            TotalActualCost = travelRequests.Where(t => t.ActualCost.HasValue).Sum(t => t.ActualCost!.Value),
            PendingVisaRequests = await _context.VisaRequests
                .CountAsync(v => v.TenantId == request.TenantId && v.Status == VisaStatus.Submitted, cancellationToken),
            RequestsByTransport = travelRequests
                .GroupBy(t => t.TransportMode)
                .ToDictionary(g => g.Key, g => g.Count())
        };
    }
}
