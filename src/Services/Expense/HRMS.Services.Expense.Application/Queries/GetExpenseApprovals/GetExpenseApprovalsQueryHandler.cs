using AutoMapper;
using HRMS.Services.Expense.Application.DTOs;
using HRMS.Services.Expense.Application.Interfaces;
using HRMS.Services.Expense.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Expense.Application.Queries.GetExpenseApprovals;

public class GetExpenseApprovalsQueryHandler : IRequestHandler<GetExpenseApprovalsQuery, List<ExpenseApprovalDto>>
{
    private readonly IExpenseDbContext _context;
    private readonly IMapper _mapper;

    public GetExpenseApprovalsQueryHandler(IExpenseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ExpenseApprovalDto>> Handle(GetExpenseApprovalsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.ExpenseApprovals
            .Where(a => a.TenantId == request.TenantId);

        if (request.ApproverId.HasValue)
            query = query.Where(a => a.ApproverId == request.ApproverId.Value);

        if (request.Status.HasValue)
            query = query.Where(a => a.Status == request.Status.Value);

        var approvals = await query
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<ExpenseApprovalDto>>(approvals);
    }
}
