using AutoMapper;
using HRMS.Services.Performance.Application.DTOs;
using HRMS.Services.Performance.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Performance.Application.Queries.GetEmployeeOKRs;

public class GetEmployeeOKRsQueryHandler : IRequestHandler<GetEmployeeOKRsQuery, List<OKRDto>>
{
    private readonly IPerformanceDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeeOKRsQueryHandler(IPerformanceDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<OKRDto>> Handle(GetEmployeeOKRsQuery request, CancellationToken cancellationToken)
    {
        var okrs = await _context.OKRs
            .Where(o => o.EmployeeId == request.EmployeeId && o.TenantId == request.TenantId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<OKRDto>>(okrs);
    }
}
