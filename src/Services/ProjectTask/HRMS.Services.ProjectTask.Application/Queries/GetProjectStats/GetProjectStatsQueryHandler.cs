using HRMS.Services.ProjectTask.Application.DTOs;
using HRMS.Services.ProjectTask.Application.Interfaces;
using HRMS.Services.ProjectTask.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Queries.GetProjectStats;

public class GetProjectStatsQueryHandler : IRequestHandler<GetProjectStatsQuery, ProjectStatsDto>
{
    private readonly IProjectTaskDbContext _context;

    public GetProjectStatsQueryHandler(IProjectTaskDbContext context)
    {
        _context = context;
    }

    public async Task<ProjectStatsDto> Handle(GetProjectStatsQuery request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == request.ProjectId, cancellationToken)
            ?? throw new InvalidOperationException($"Project with ID {request.ProjectId} not found.");

        var memberCount = await _context.ProjectMembers.CountAsync(m => m.ProjectId == request.ProjectId, cancellationToken);
        var epicCount = await _context.Epics.CountAsync(e => e.ProjectId == request.ProjectId, cancellationToken);
        var stories = await _context.Stories.Where(s => s.ProjectId == request.ProjectId).ToListAsync(cancellationToken);
        var tasks = await _context.TaskItems.Where(t => t.ProjectId == request.ProjectId).ToListAsync(cancellationToken);
        var bugs = await _context.Bugs.Where(b => b.ProjectId == request.ProjectId).ToListAsync(cancellationToken);
        var timeLogs = await _context.TimeLogs.Where(t => t.StoryId.HasValue && stories.Select(s => s.Id).Contains(t.StoryId.Value)).SumAsync(t => t.Hours, cancellationToken);

        return new ProjectStatsDto
        {
            ProjectId = request.ProjectId,
            TotalMembers = memberCount,
            TotalEpics = epicCount,
            TotalStories = stories.Count,
            TotalTasks = tasks.Count,
            TotalBugs = bugs.Count,
            OpenBugs = bugs.Count(b => b.Status == BugStatus.Open || b.Status == BugStatus.InProgress),
            CompletedTasks = tasks.Count(t => t.Status == Domain.Enums.TaskStatus.Done),
            InProgressTasks = tasks.Count(t => t.Status == Domain.Enums.TaskStatus.InProgress),
            TodoTasks = tasks.Count(t => t.Status == Domain.Enums.TaskStatus.Todo),
            TotalLoggedHours = timeLogs,
            ProgressPercentage = project.ProgressPercentage
        };
    }
}
