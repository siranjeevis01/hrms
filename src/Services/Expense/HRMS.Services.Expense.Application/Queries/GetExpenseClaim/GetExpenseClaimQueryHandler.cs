using AutoMapper;
using HRMS.Services.Expense.Application.DTOs;
using HRMS.Services.Expense.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Expense.Application.Queries.GetExpenseClaim;

public class GetExpenseClaimQueryHandler : IRequestHandler<GetExpenseClaimQuery, ExpenseClaimDto?>
{
    private readonly IExpenseDbContext _context;
    private readonly IMapper _mapper;

    public GetExpenseClaimQueryHandler(IExpenseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ExpenseClaimDto?> Handle(GetExpenseClaimQuery request, CancellationToken cancellationToken)
    {
        var claim = await _context.ExpenseClaims
            .Include(c => c.Items)
            .Include(c => c.Approvals)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (claim == null)
            return null;

        return _mapper.Map<ExpenseClaimDto>(claim);
    }
}
