using AutoMapper;
using HRMS.Services.Expense.Application.DTOs;
using HRMS.Services.Expense.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Expense.Application.Queries.GetExpenseCategories;

public class GetExpenseCategoriesQueryHandler : IRequestHandler<GetExpenseCategoriesQuery, List<ExpenseCategoryDto>>
{
    private readonly IExpenseDbContext _context;
    private readonly IMapper _mapper;

    public GetExpenseCategoriesQueryHandler(IExpenseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ExpenseCategoryDto>> Handle(GetExpenseCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _context.ExpenseCategories
            .Where(c => c.TenantId == request.TenantId && c.IsActive)
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<ExpenseCategoryDto>>(categories);
    }
}
