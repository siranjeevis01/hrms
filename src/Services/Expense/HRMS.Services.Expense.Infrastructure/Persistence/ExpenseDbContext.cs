using HRMS.Services.Expense.Application.Interfaces;
using HRMS.Services.Expense.Domain.Entities;
using HRMS.Shared.Kernel.Common;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Expense.Infrastructure.Persistence;

public class ExpenseDbContext : DbContext, IExpenseDbContext
{
    public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options) : base(options) { }

    public DbSet<ExpenseClaim> ExpenseClaims => Set<ExpenseClaim>();
    public DbSet<ExpenseItem> ExpenseItems => Set<ExpenseItem>();
    public DbSet<ExpensePolicy> ExpensePolicies => Set<ExpensePolicy>();
    public DbSet<ExpenseCategoryEntity> ExpenseCategories => Set<ExpenseCategoryEntity>();
    public DbSet<ExpenseApproval> ExpenseApprovals => Set<ExpenseApproval>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExpenseDbContext).Assembly);
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
