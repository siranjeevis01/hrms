using AutoMapper;
using HRMS.Services.ProjectTask.Application.DTOs;
using HRMS.Services.ProjectTask.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Queries.GetSprintTasks;

public class GetSprintTasksQueryHandler : IRequestHandler<GetSprintTasksQuery, List<TaskItemDto>>
{
    private readonly IProjectTaskDbContext _context;
    private readonly IMapper _mapper;

    public GetSprintTasksQueryHandler(IProjectTaskDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TaskItemDto>> Handle(GetSprintTasksQuery request, CancellationToken cancellationToken)
    {
        var sprint = await _context.Sprints
            .FirstOrDefaultAsync(s => s.Id == request.SprintId, cancellationToken);

        if (sprint == null)
            return new List<TaskItemDto>();

        var storyIds = await _context.Stories
            .Where(s => s.SprintId == request.SprintId)
            .Select(s => s.Id)
            .ToListAsync(cancellationToken);

        var taskIds = await _context.TaskItems
            .Where(t => t.ProjectId == sprint.ProjectId && t.StoryId != null)
            .ToListAsync(cancellationToken);

        var tasks = taskIds.Where(t => t.StoryId.HasValue && storyIds.Contains(t.StoryId.Value)).ToList();

        return _mapper.Map<List<TaskItemDto>>(tasks);
    }
}
