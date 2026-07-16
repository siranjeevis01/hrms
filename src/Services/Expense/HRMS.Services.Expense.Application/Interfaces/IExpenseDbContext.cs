using HRMS.Services.Expense.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Expense.Application.Interfaces;

public interface IExpenseDbContext
{
    DbSet<ExpenseClaim> ExpenseClaims { get; }
    DbSet<ExpenseItem> ExpenseItems { get; }
    DbSet<ExpensePolicy> ExpensePolicies { get; }
    DbSet<ExpenseCategoryEntity> ExpenseCategories { get; }
    DbSet<ExpenseApproval> ExpenseApprovals { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
