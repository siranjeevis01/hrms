using AutoMapper;
using HRMS.Services.Expense.Application.DTOs;
using HRMS.Services.Expense.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Expense.Application.Queries.GetExpenseClaims;

public class GetExpenseClaimsQueryHandler : IRequestHandler<GetExpenseClaimsQuery, PagedResult<ExpenseClaimDto>>
{
    private readonly IExpenseDbContext _context;
    private readonly IMapper _mapper;

    public GetExpenseClaimsQueryHandler(IExpenseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<ExpenseClaimDto>> Handle(GetExpenseClaimsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.ExpenseClaims
            .Include(c => c.Items)
            .AsQueryable();

        if (request.EmployeeId.HasValue)
            query = query.Where(c => c.EmployeeId == request.EmployeeId.Value);

        if (request.Status.HasValue)
            query = query.Where(c => c.Status == request.Status.Value);

        if (request.FromDate.HasValue)
            query = query.Where(c => c.CreatedAt >= request.FromDate.Value);

        if (request.ToDate.HasValue)
            query = query.Where(c => c.CreatedAt <= request.ToDate.Value);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var search = request.SearchTerm.ToLower();
            query = query.Where(c =>
                c.Title.ToLower().Contains(search) ||
                c.Description!.ToLower().Contains(search));
        }

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
