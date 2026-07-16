using AutoMapper;
using AutoMapper.QueryableExtensions;
using HRMS.Services.Payroll.Application.DTOs;
using HRMS.Services.Payroll.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Payroll.Application.Queries.GetEmployeeLoans;

public class GetEmployeeLoansQueryHandler : IRequestHandler<GetEmployeeLoansQuery, List<LoanDto>>
{
    private readonly IPayrollDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeeLoansQueryHandler(IPayrollDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<LoanDto>> Handle(GetEmployeeLoansQuery request, CancellationToken cancellationToken)
    {
        return await _context.Loans
            .Include(l => l.Repayments)
            .Where(l => l.EmployeeId == request.EmployeeId)
            .OrderByDescending(l => l.StartDate)
            .ProjectTo<LoanDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
