using AutoMapper;
using HRMS.Services.Performance.Application.DTOs;
using HRMS.Services.Performance.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Performance.Application.Queries.GetEmployeeGoals;

public class GetEmployeeGoalsQueryHandler : IRequestHandler<GetEmployeeGoalsQuery, List<GoalDto>>
{
    private readonly IPerformanceDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeeGoalsQueryHandler(IPerformanceDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GoalDto>> Handle(GetEmployeeGoalsQuery request, CancellationToken cancellationToken)
    {
        var goals = await _context.Goals
            .Where(g => g.EmployeeId == request.EmployeeId && g.TenantId == request.TenantId)
            .OrderByDescending(g => g.CreatedAt)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<GoalDto>>(goals);
    }
}
