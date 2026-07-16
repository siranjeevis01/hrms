using AutoMapper;
using HRMS.Services.ProjectTask.Application.DTOs;
using HRMS.Services.ProjectTask.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Queries.GetBacklog;

public class GetBacklogQueryHandler : IRequestHandler<GetBacklogQuery, GetBacklogResult>
{
    private readonly IProjectTaskDbContext _context;
    private readonly IMapper _mapper;

    public GetBacklogQueryHandler(IProjectTaskDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetBacklogResult> Handle(GetBacklogQuery request, CancellationToken cancellationToken)
    {
        var unassignedStories = await _context.Stories
            .Where(s => s.ProjectId == request.ProjectId && s.AssignedTo == null)
            .OrderBy(s => s.CreatedAt)
            .ToListAsync(cancellationToken);

        var unassignedTasks = await _context.TaskItems
            .Where(t => t.ProjectId == request.ProjectId && t.AssignedTo == null)
            .OrderBy(t => t.CreatedAt)
            .ToListAsync(cancellationToken);

        return new GetBacklogResult
        {
            UnassignedStories = _mapper.Map<List<StoryDto>>(unassignedStories),
            UnassignedTasks = _mapper.Map<List<TaskItemDto>>(unassignedTasks)
        };
    }
}
