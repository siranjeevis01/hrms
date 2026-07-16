using AutoMapper;
using HRMS.Services.Payroll.Application.DTOs;
using HRMS.Services.Payroll.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Payroll.Application.Queries.GetLoanDetails;

public class GetLoanDetailsQueryHandler : IRequestHandler<GetLoanDetailsQuery, LoanDto?>
{
    private readonly IPayrollDbContext _context;
    private readonly IMapper _mapper;

    public GetLoanDetailsQueryHandler(IPayrollDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<LoanDto?> Handle(GetLoanDetailsQuery request, CancellationToken cancellationToken)
    {
        var loan = await _context.Loans
            .Include(l => l.Repayments)
            .FirstOrDefaultAsync(l => l.Id == request.LoanId, cancellationToken);

        return loan == null ? null : _mapper.Map<LoanDto>(loan);
    }
}
