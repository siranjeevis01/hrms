using HRMS.Services.ProjectTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Application.Interfaces;

public interface IProjectTaskDbContext
{
    DbSet<Domain.Entities.Project> Projects { get; }
    DbSet<ProjectMember> ProjectMembers { get; }
    DbSet<Epic> Epics { get; }
    DbSet<Story> Stories { get; }
    DbSet<TaskItem> TaskItems { get; }
    DbSet<Bug> Bugs { get; }
    DbSet<Sprint> Sprints { get; }
    DbSet<Board> Boards { get; }
    DbSet<Comment> Comments { get; }
    DbSet<TimeLog> TimeLogs { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
