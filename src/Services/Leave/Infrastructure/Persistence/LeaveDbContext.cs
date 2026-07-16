using HRMS.Services.Leave.Application.Interfaces;
using HRMS.Services.Leave.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using HRMS.Shared.Kernel.Interfaces;

namespace HRMS.Services.Leave.Infrastructure.Persistence;

public class LeaveDbContext : DbContext, ILeaveDbContext
{
    private readonly ITenantService? _tenantService;

    public LeaveDbContext(DbContextOptions<LeaveDbContext> options, ITenantService? tenantService = null)
        : base(options)
    {
        _tenantService = tenantService;
    }

    public DbSet<LeaveType> LeaveTypes => Set<LeaveType>();
    public DbSet<LeaveBalance> LeaveBalances => Set<LeaveBalance>();
    public DbSet<LeaveApplication> LeaveApplications => Set<LeaveApplication>();
    public DbSet<LeaveComment> LeaveComments => Set<LeaveComment>();
    public DbSet<LeaveApprovalMatrix> LeaveApprovalMatrices => Set<LeaveApprovalMatrix>();
    public DbSet<LeaveAccrualPolicy> LeaveAccrualPolicies => Set<LeaveAccrualPolicy>();
    public DbSet<CompOff> CompOffs => Set<CompOff>();
    public DbSet<LeavePolicy> LeavePolicies => Set<LeavePolicy>();
    public DbSet<HolidayCalendarEntry> HolidayCalendarEntries => Set<HolidayCalendarEntry>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LeaveDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>()
            .HaveMaxLength(500);
    }

    public Guid? GetCurrentTenantId()
    {
        return _tenantService?.GetCurrentTenant()?.Id;
    }
}
