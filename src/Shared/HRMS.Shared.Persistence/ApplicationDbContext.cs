using System.Reflection;
using HRMS.Shared.Kernel.Common;
using HRMS.Shared.Kernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Shared.Persistence;

public class ApplicationDbContext : DbContext
{
    private readonly ITenantService? _tenantService;

    protected ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ITenantService tenantService)
        : base(options)
    {
        _tenantService = tenantService;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
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
