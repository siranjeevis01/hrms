using HRMS.Services.Performance.Application.Interfaces;
using HRMS.Services.Performance.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Performance.Infrastructure.Persistence;

public class PerformanceDbContext : DbContext, IPerformanceDbContext
{
    public PerformanceDbContext(DbContextOptions<PerformanceDbContext> options) : base(options) { }

    public DbSet<Goal> Goals => Set<Goal>();
    public DbSet<KeyResult> KeyResults => Set<KeyResult>();
    public DbSet<OKR> OKRs => Set<OKR>();
    public DbSet<OKRItem> OKRItems => Set<OKRItem>();
    public DbSet<KPI> KPIs => Set<KPI>();
    public DbSet<PerformanceReview> PerformanceReviews => Set<PerformanceReview>();
    public DbSet<ReviewCriteria> ReviewCriteria => Set<ReviewCriteria>();
    public DbSet<Feedback360> Feedback360s => Set<Feedback360>();
    public DbSet<FeedbackAnswer> FeedbackAnswers => Set<FeedbackAnswer>();
    public DbSet<CalibrationSession> CalibrationSessions => Set<CalibrationSession>();
    public DbSet<CalibrationEntry> CalibrationEntries => Set<CalibrationEntry>();
    public DbSet<Appraisal> Appraisals => Set<Appraisal>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PerformanceDbContext).Assembly);
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
