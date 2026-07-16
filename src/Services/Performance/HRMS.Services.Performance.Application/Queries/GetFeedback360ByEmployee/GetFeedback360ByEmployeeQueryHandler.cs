using AutoMapper;
using HRMS.Services.Performance.Application.DTOs;
using HRMS.Services.Performance.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Performance.Application.Queries.GetFeedback360ByEmployee;

public class GetFeedback360ByEmployeeQueryHandler : IRequestHandler<GetFeedback360ByEmployeeQuery, List<Feedback360Dto>>
{
    private readonly IPerformanceDbContext _context;
    private readonly IMapper _mapper;

    public GetFeedback360ByEmployeeQueryHandler(IPerformanceDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Feedback360Dto>> Handle(GetFeedback360ByEmployeeQuery request, CancellationToken cancellationToken)
    {
        var feedbacks = await _context.Feedback360s
            .Where(f => f.EmployeeId == request.EmployeeId && f.TenantId == request.TenantId)
            .OrderByDescending(f => f.CreatedAt)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<Feedback360Dto>>(feedbacks);
    }
}
