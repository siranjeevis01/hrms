using AutoMapper;
using HRMS.Services.Performance.Application.DTOs;
using HRMS.Services.Performance.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Performance.Application.Queries.GetEmployeeAppraisals;

public class GetEmployeeAppraisalsQueryHandler : IRequestHandler<GetEmployeeAppraisalsQuery, List<AppraisalDto>>
{
    private readonly IPerformanceDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeeAppraisalsQueryHandler(IPerformanceDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<AppraisalDto>> Handle(GetEmployeeAppraisalsQuery request, CancellationToken cancellationToken)
    {
        var appraisals = await _context.Appraisals
            .Where(a => a.EmployeeId == request.EmployeeId && a.TenantId == request.TenantId)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<AppraisalDto>>(appraisals);
    }
}
