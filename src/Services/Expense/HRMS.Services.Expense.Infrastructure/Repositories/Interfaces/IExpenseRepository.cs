using HRMS.Services.Expense.Application.DTOs;
using HRMS.Services.Expense.Domain.Enums;

namespace HRMS.Services.Expense.Infrastructure.Repositories.Interfaces;

public interface IExpenseRepository
{
    Task<List<ExpenseClaimDto>> GetPagedWithFiltersAsync(
        int pageNumber, int pageSize, Guid? employeeId,
        ClaimStatus? status, ClaimCategory? category,
        DateTime? fromDate, DateTime? toDate, string? searchTerm,
        CancellationToken cancellationToken = default);
}
