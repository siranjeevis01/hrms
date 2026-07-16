using HRMS.Services.Audit.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Audit.Application.Interfaces;

public interface IAuditDbContext
{
    DbSet<AuditLog> AuditLogs { get; }
    DbSet<AuditTrail> AuditTrails { get; }
    DbSet<LoginHistory> LoginHistories { get; }
    DbSet<DataChangeLog> DataChangeLogs { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
