using AutoMapper;
using HRMS.Services.Expense.Application.DTOs;
using HRMS.Services.Expense.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Expense.Application.Queries.GetExpenseItems;

public class GetExpenseItemsQueryHandler : IRequestHandler<GetExpenseItemsQuery, List<ExpenseItemDto>>
{
    private readonly IExpenseDbContext _context;
    private readonly IMapper _mapper;

    public GetExpenseItemsQueryHandler(IExpenseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ExpenseItemDto>> Handle(GetExpenseItemsQuery request, CancellationToken cancellationToken)
    {
        var items = await _context.ExpenseItems
            .Where(i => i.ClaimId == request.ClaimId)
            .OrderBy(i => i.Date)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<ExpenseItemDto>>(items);
    }
}
