using HRMS.Services.Expense.Application.DTOs;
using HRMS.Services.Expense.Domain.Enums;
using HRMS.Services.Expense.Infrastructure.Repositories.Interfaces;
using HRMS.Services.Expense.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Expense.Infrastructure.Repositories;

public class ExpenseRepository : IExpenseRepository
{
    private readonly IExpenseDbContext _context;

    public ExpenseRepository(IExpenseDbContext context)
    {
        _context = context;
    }

    public async Task<List<ExpenseClaimDto>> GetPagedWithFiltersAsync(
        int pageNumber, int pageSize, Guid? employeeId,
        ClaimStatus? status, ClaimCategory? category,
        DateTime? fromDate, DateTime? toDate, string? searchTerm,
        CancellationToken cancellationToken = default)
    {
        var query = _context.ExpenseClaims.AsQueryable();

        if (employeeId.HasValue)
            query = query.Where(c => c.EmployeeId == employeeId.Value);

        if (status.HasValue)
            query = query.Where(c => c.Status == status.Value);

        if (fromDate.HasValue)
            query = query.Where(c => c.CreatedAt >= fromDate.Value);

        if (toDate.HasValue)
            query = query.Where(c => c.CreatedAt <= toDate.Value);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var search = searchTerm.ToLower();
            query = query.Where(c =>
                c.Title.ToLower().Contains(search) ||
                c.Description!.ToLower().Contains(search));
        }

        return await query
            .OrderByDescending(c => c.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(c => new ExpenseClaimDto
            {
                Id = c.Id,
                EmployeeId = c.EmployeeId,
                Title = c.Title,
                Description = c.Description,
                TotalAmount = c.TotalAmount,
                Currency = c.Currency,
                Status = c.Status,
                SubmittedAt = c.SubmittedAt,
                ReviewedBy = c.ReviewedBy,
                ReviewedAt = c.ReviewedAt,
                RejectionReason = c.RejectionReason,
                PolicyViolationNotes = c.PolicyViolationNotes,
                TenantId = c.TenantId,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
