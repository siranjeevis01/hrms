using HRMS.Services.Report.Application.Interfaces;
using HRMS.Services.Report.Domain.Entities;
using HRMS.Shared.Kernel.Common;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Report.Infrastructure.Persistence;

public class ReportDbContext : DbContext, IReportDbContext
{
    public ReportDbContext(DbContextOptions<ReportDbContext> options) : base(options) { }

    public DbSet<ReportTemplate> ReportTemplates => Set<ReportTemplate>();
    public DbSet<ReportInstance> ReportInstances => Set<ReportInstance>();
    public DbSet<ScheduledReport> ScheduledReports => Set<ScheduledReport>();
    public DbSet<ReportAccess> ReportAccesses => Set<ReportAccess>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReportDbContext).Assembly);
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
