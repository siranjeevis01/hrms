using AutoMapper;
using HRMS.Services.Travel.Application.DTOs;
using HRMS.Services.Travel.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Travel.Application.Queries.GetVisaRequests;

public class GetVisaRequestsQueryHandler : IRequestHandler<GetVisaRequestsQuery, List<VisaRequestDto>>
{
    private readonly ITravelDbContext _context;
    private readonly IMapper _mapper;

    public GetVisaRequestsQueryHandler(ITravelDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<VisaRequestDto>> Handle(GetVisaRequestsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.VisaRequests
            .Where(v => v.TenantId == request.TenantId);

        if (request.EmployeeId.HasValue)
            query = query.Where(v => v.EmployeeId == request.EmployeeId.Value);

        var visaRequests = await query
            .OrderByDescending(v => v.CreatedAt)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<VisaRequestDto>>(visaRequests);
    }
}
