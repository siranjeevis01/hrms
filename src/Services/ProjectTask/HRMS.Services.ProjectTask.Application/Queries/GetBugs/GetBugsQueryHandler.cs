using AutoMapper;
using HRMS.Services.ProjectTask.Application.DTOs;
using HRMS.Services.ProjectTask.Application.Interfaces;
using HRMS.Shared.Kernel.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Queries.GetBugs;

public class GetBugsQueryHandler : IRequestHandler<GetBugsQuery, PagedResult<BugDto>>
{
    private readonly IProjectTaskDbContext _context;
    private readonly IMapper _mapper;

    public GetBugsQueryHandler(IProjectTaskDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<BugDto>> Handle(GetBugsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Bugs.AsQueryable();

        if (request.ProjectId.HasValue)
            query = query.Where(b => b.ProjectId == request.ProjectId.Value);

        if (request.AssignedTo.HasValue)
            query = query.Where(b => b.AssignedTo == request.AssignedTo.Value);

        if (request.Status.HasValue)
            query = query.Where(b => b.Status == request.Status.Value);

        if (request.Severity.HasValue)
            query = query.Where(b => b.Severity == request.Severity.Value);

        var totalCount = await query.CountAsync(cancellationToken);

        var bugs = await query
            .OrderByDescending(b => b.Severity)
            .ThenBy(b => b.CreatedAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var items = _mapper.Map<List<BugDto>>(bugs);

        return PagedResult<BugDto>.Create(items, totalCount, request.PageNumber, request.PageSize);
    }
}
