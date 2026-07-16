using HRMS.Services.Attendance.Application.Interfaces;
using HRMS.Services.Attendance.Domain.Entities;
using HRMS.Shared.Kernel.Common;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Attendance.Infrastructure.Persistence;

public class AttendanceDbContext : DbContext, IAttendanceDbContext
{
    public AttendanceDbContext(DbContextOptions<AttendanceDbContext> options) : base(options) { }

    public DbSet<AttendanceRecord> AttendanceRecords => Set<AttendanceRecord>();
    public DbSet<ShiftAssignment> ShiftAssignments => Set<ShiftAssignment>();
    public DbSet<GeoFence> GeoFences => Set<GeoFence>();
    public DbSet<WifiNetwork> WifiNetworks => Set<WifiNetwork>();
    public DbSet<AttendanceRegularization> AttendanceRegularizations => Set<AttendanceRegularization>();
    public DbSet<WorkFromHome> WorkFromHomes => Set<WorkFromHome>();
    public DbSet<AttendanceSummary> AttendanceSummaries => Set<AttendanceSummary>();
    public DbSet<AttendancePolicy> AttendancePolicies => Set<AttendancePolicy>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AttendanceDbContext).Assembly);
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

        foreach (var entry in ChangeTracker.Entries<AggregateRoot>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
