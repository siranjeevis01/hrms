using HRMS.Services.Expense.Application.DTOs;
using HRMS.Services.Expense.Application.Interfaces;
using HRMS.Services.Expense.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Expense.Application.Queries.GetExpenseStats;

public class GetExpenseStatsQueryHandler : IRequestHandler<GetExpenseStatsQuery, ExpenseStatsDto>
{
    private readonly IExpenseDbContext _context;

    public GetExpenseStatsQueryHandler(IExpenseDbContext context)
    {
        _context = context;
    }

    public async Task<ExpenseStatsDto> Handle(GetExpenseStatsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.ExpenseClaims
            .Where(c => c.TenantId == request.TenantId);

        if (request.EmployeeId.HasValue)
            query = query.Where(c => c.EmployeeId == request.EmployeeId.Value);

        if (request.FromDate.HasValue)
            query = query.Where(c => c.CreatedAt >= request.FromDate.Value);

        if (request.ToDate.HasValue)
            query = query.Where(c => c.CreatedAt <= request.ToDate.Value);

        var claims = await query.ToListAsync(cancellationToken);

        return new ExpenseStatsDto
        {
            TotalClaims = claims.Count,
            DraftClaims = claims.Count(c => c.Status == ClaimStatus.Draft),
            SubmittedClaims = claims.Count(c => c.Status == ClaimStatus.Submitted),
            ApprovedClaims = claims.Count(c => c.Status == ClaimStatus.Approved),
            RejectedClaims = claims.Count(c => c.Status == ClaimStatus.Rejected),
            PaidClaims = claims.Count(c => c.Status == ClaimStatus.Paid),
            TotalAmount = claims.Sum(c => c.TotalAmount),
            ApprovedAmount = claims.Where(c => c.Status == ClaimStatus.Approved).Sum(c => c.TotalAmount),
            PendingAmount = claims.Where(c => c.Status == ClaimStatus.Submitted || c.Status == ClaimStatus.UnderReview).Sum(c => c.TotalAmount),
            RejectedAmount = claims.Where(c => c.Status == ClaimStatus.Rejected).Sum(c => c.TotalAmount)
        };
    }
}
