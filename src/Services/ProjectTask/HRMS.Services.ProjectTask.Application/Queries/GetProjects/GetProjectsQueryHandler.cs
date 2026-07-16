using AutoMapper;
using HRMS.Services.ProjectTask.Application.DTOs;
using HRMS.Services.ProjectTask.Application.Interfaces;
using HRMS.Shared.Kernel.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Queries.GetProjects;

public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, PagedResult<ProjectListDto>>
{
    private readonly IProjectTaskDbContext _context;
    private readonly IMapper _mapper;

    public GetProjectsQueryHandler(IProjectTaskDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<ProjectListDto>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Projects.Include(p => p.Members).AsQueryable();

        if (request.Status.HasValue)
            query = query.Where(p => p.Status == request.Status.Value);

        if (request.DepartmentId.HasValue)
            query = query.Where(p => p.DepartmentId == request.DepartmentId.Value);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var search = request.SearchTerm.ToLower();
            query = query.Where(p =>
                p.Name.ToLower().Contains(search) ||
                p.Code.ToLower().Contains(search));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        IOrderedQueryable<Domain.Entities.Project> orderedQuery;
        if (request.SortBy?.ToLower() == "priority")
            orderedQuery = query.OrderBy(p => p.Priority);
        else if (request.SortBy?.ToLower() == "status")
            orderedQuery = query.OrderBy(p => p.Status);
        else if (request.SortBy?.ToLower() == "enddate")
            orderedQuery = query.OrderBy(p => p.EndDate);
        else
            orderedQuery = query.OrderByDescending(p => p.CreatedAt);

        var projects = await orderedQuery
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var items = _mapper.Map<List<ProjectListDto>>(projects);

        return PagedResult<ProjectListDto>.Create(items, totalCount, request.PageNumber, request.PageSize);
    }
}
