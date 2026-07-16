using HRMS.Services.Audit.Application.Interfaces;
using HRMS.Services.Audit.Domain.Entities;
using HRMS.Shared.Kernel.Common;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Audit.Infrastructure.Persistence;

public class AuditDbContext : DbContext, IAuditDbContext
{
    public AuditDbContext(DbContextOptions<AuditDbContext> options) : base(options) { }

    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<AuditTrail> AuditTrails => Set<AuditTrail>();
    public DbSet<LoginHistory> LoginHistories => Set<LoginHistory>();
    public DbSet<DataChangeLog> DataChangeLogs => Set<DataChangeLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuditDbContext).Assembly);
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
