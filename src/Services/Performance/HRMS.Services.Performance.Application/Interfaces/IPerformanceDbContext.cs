using HRMS.Services.Performance.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Performance.Application.Interfaces;

public interface IPerformanceDbContext
{
    DbSet<Goal> Goals { get; }
    DbSet<KeyResult> KeyResults { get; }
    DbSet<OKR> OKRs { get; }
    DbSet<OKRItem> OKRItems { get; }
    DbSet<KPI> KPIs { get; }
    DbSet<PerformanceReview> PerformanceReviews { get; }
    DbSet<ReviewCriteria> ReviewCriteria { get; }
    DbSet<Feedback360> Feedback360s { get; }
    DbSet<FeedbackAnswer> FeedbackAnswers { get; }
    DbSet<CalibrationSession> CalibrationSessions { get; }
    DbSet<CalibrationEntry> CalibrationEntries { get; }
    DbSet<Appraisal> Appraisals { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
