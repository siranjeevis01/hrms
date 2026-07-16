using AutoMapper;
using HRMS.Services.Performance.Application.DTOs;
using HRMS.Services.Performance.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace HRMS.Services.Performance.Application.Queries.GetGoal;

public class GetGoalQueryHandler : IRequestHandler<GetGoalQuery, GoalDto?>
{
    private readonly IPerformanceDbContext _context;
    private readonly IMapper _mapper;

    public GetGoalQueryHandler(IPerformanceDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GoalDto?> Handle(GetGoalQuery request, CancellationToken cancellationToken)
    {
        var goal = await _context.Goals
            .Include(g => g.KeyResults)
            .FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);

        if (goal == null)
            return null;

        return _mapper.Map<GoalDto>(goal);
    }
}
