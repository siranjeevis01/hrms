using AutoMapper;
using HRMS.Services.Expense.Application.DTOs;
using HRMS.Services.Expense.Application.Interfaces;
using HRMS.Services.Expense.Application.Queries.GetExpenseClaims;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Expense.Application.Queries.GetEmployeeExpenseClaims;

public class GetEmployeeExpenseClaimsQueryHandler : IRequestHandler<GetEmployeeExpenseClaimsQuery, PagedResult<ExpenseClaimDto>>
{
    private readonly IExpenseDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeeExpenseClaimsQueryHandler(IExpenseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<ExpenseClaimDto>> Handle(GetEmployeeExpenseClaimsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.ExpenseClaims
            .Where(c => c.EmployeeId == request.EmployeeId);

        if (request.Status.HasValue)
            query = query.Where(c => c.Status == request.Status.Value);

        var totalCount = await query.CountAsync(cancellationToken);

        var claims = await query
            .OrderByDescending(c => c.CreatedAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var items = _mapper.Map<List<ExpenseClaimDto>>(claims);

        return new PagedResult<ExpenseClaimDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}
