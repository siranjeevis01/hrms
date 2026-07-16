using HRMS.Services.ProjectTask.Application.Interfaces;
using HRMS.Services.ProjectTask.Domain.Entities;
using HRMS.Shared.Kernel.Common;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.ProjectTask.Infrastructure.Persistence;

public class ProjectTaskDbContext : DbContext, IProjectTaskDbContext
{
    public ProjectTaskDbContext(DbContextOptions<ProjectTaskDbContext> options) : base(options) { }

    public DbSet<Domain.Entities.Project> Projects => Set<Domain.Entities.Project>();
    public DbSet<ProjectMember> ProjectMembers => Set<ProjectMember>();
    public DbSet<Epic> Epics => Set<Epic>();
    public DbSet<Story> Stories => Set<Story>();
    public DbSet<TaskItem> TaskItems => Set<TaskItem>();
    public DbSet<Bug> Bugs => Set<Bug>();
    public DbSet<Sprint> Sprints => Set<Sprint>();
    public DbSet<Board> Boards => Set<Board>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<TimeLog> TimeLogs => Set<TimeLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjectTaskDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
