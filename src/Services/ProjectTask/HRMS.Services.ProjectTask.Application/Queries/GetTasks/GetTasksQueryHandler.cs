using AutoMapper;
using HRMS.Services.ProjectTask.Application.DTOs;
using HRMS.Services.ProjectTask.Application.Interfaces;
using HRMS.Shared.Kernel.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Queries.GetTasks;

public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, PagedResult<TaskItemDto>>
{
    private readonly IProjectTaskDbContext _context;
    private readonly IMapper _mapper;

    public GetTasksQueryHandler(IProjectTaskDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<TaskItemDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        var query = _context.TaskItems.AsQueryable();

        if (request.ProjectId.HasValue)
            query = query.Where(t => t.ProjectId == request.ProjectId.Value);

        if (request.StoryId.HasValue)
            query = query.Where(t => t.StoryId == request.StoryId.Value);

        if (request.AssignedTo.HasValue)
            query = query.Where(t => t.AssignedTo == request.AssignedTo.Value);

        if (request.Status.HasValue)
            query = query.Where(t => t.Status == request.Status.Value);

        var totalCount = await query.CountAsync(cancellationToken);

        var tasks = await query
            .OrderBy(t => t.CreatedAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var items = _mapper.Map<List<TaskItemDto>>(tasks);

        return PagedResult<TaskItemDto>.Create(items, totalCount, request.PageNumber, request.PageSize);
    }
}
