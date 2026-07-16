using HRMS.Services.Travel.Application.Interfaces;
using HRMS.Services.Travel.Domain.Entities;
using HRMS.Shared.Kernel.Common;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Travel.Infrastructure.Persistence;

public class TravelDbContext : DbContext, ITravelDbContext
{
    public TravelDbContext(DbContextOptions<TravelDbContext> options) : base(options) { }

    public DbSet<TravelRequest> TravelRequests => Set<TravelRequest>();
    public DbSet<TravelItinerary> TravelItineraries => Set<TravelItinerary>();
    public DbSet<TravelExpense> TravelExpenses => Set<TravelExpense>();
    public DbSet<TravelApproval> TravelApprovals => Set<TravelApproval>();
    public DbSet<VisaRequest> VisaRequests => Set<VisaRequest>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TravelDbContext).Assembly);
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
