using HRMS.Services.Expense.Application.DTOs;
using MediatR;

namespace HRMS.Services.Expense.Application.Queries.GetExpenseStats;

public class GetExpenseStatsQuery : IRequest<ExpenseStatsDto>
{
    public Guid? EmployeeId { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
