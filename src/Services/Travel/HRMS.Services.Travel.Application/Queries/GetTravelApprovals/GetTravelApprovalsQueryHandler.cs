using AutoMapper;
using HRMS.Services.Travel.Application.DTOs;
using HRMS.Services.Travel.Application.Interfaces;
using HRMS.Services.Travel.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Travel.Application.Queries.GetTravelApprovals;

public class GetTravelApprovalsQueryHandler : IRequestHandler<GetTravelApprovalsQuery, List<TravelApprovalDto>>
{
    private readonly ITravelDbContext _context;
    private readonly IMapper _mapper;

    public GetTravelApprovalsQueryHandler(ITravelDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TravelApprovalDto>> Handle(GetTravelApprovalsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.TravelApprovals
            .Where(a => a.TenantId == request.TenantId);

        if (request.ApproverId.HasValue)
            query = query.Where(a => a.ApproverId == request.ApproverId.Value);

        if (request.Status.HasValue)
            query = query.Where(a => a.Status == request.Status.Value);

        var approvals = await query
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<TravelApprovalDto>>(approvals);
    }
}
