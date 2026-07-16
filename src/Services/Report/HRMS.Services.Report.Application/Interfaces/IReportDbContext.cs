using HRMS.Services.Report.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Report.Application.Interfaces;

public interface IReportDbContext
{
    DbSet<ReportTemplate> ReportTemplates { get; }
    DbSet<ReportInstance> ReportInstances { get; }
    DbSet<ScheduledReport> ScheduledReports { get; }
    DbSet<ReportAccess> ReportAccesses { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
