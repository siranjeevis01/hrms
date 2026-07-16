using AutoMapper;
using HRMS.Services.Performance.Application.DTOs;
using HRMS.Services.Performance.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Performance.Application.Queries.GetGoals;

public class GetGoalsQueryHandler : IRequestHandler<GetGoalsQuery, PagedResult<GoalDto>>
{
    private readonly IPerformanceDbContext _context;
    private readonly IMapper _mapper;

    public GetGoalsQueryHandler(IPerformanceDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<GoalDto>> Handle(GetGoalsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Goals
            .Where(g => g.TenantId == request.TenantId)
            .AsQueryable();

        if (request.Category.HasValue)
            query = query.Where(g => g.Category == request.Category.Value);

        if (request.Status.HasValue)
            query = query.Where(g => g.Status == request.Status.Value);

        if (request.DepartmentId.HasValue)
            query = query.Where(g => g.DepartmentId == request.DepartmentId.Value);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var search = request.SearchTerm.ToLower();
            query = query.Where(g => g.Title.ToLower().Contains(search));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var goals = await query
            .OrderByDescending(g => g.CreatedAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var items = _mapper.Map<List<GoalDto>>(goals);

        return new PagedResult<GoalDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}
