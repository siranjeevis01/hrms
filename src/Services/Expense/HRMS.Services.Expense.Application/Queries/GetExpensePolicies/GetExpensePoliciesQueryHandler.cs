using AutoMapper;
using HRMS.Services.Expense.Application.DTOs;
using HRMS.Services.Expense.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Expense.Application.Queries.GetExpensePolicies;

public class GetExpensePoliciesQueryHandler : IRequestHandler<GetExpensePoliciesQuery, List<ExpensePolicyDto>>
{
    private readonly IExpenseDbContext _context;
    private readonly IMapper _mapper;

    public GetExpensePoliciesQueryHandler(IExpenseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ExpensePolicyDto>> Handle(GetExpensePoliciesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.ExpensePolicies
            .Where(p => p.TenantId == request.TenantId);

        if (request.IsActive.HasValue)
            query = query.Where(p => p.IsActive == request.IsActive.Value);

        var policies = await query
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<ExpensePolicyDto>>(policies);
    }
}
