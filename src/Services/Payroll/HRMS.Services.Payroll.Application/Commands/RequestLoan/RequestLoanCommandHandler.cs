using HRMS.Services.Payroll.Application.Interfaces;
using HRMS.Services.Payroll.Domain.Entities;
using MediatR;

namespace HRMS.Services.Payroll.Application.Commands.RequestLoan;

public class RequestLoanCommandHandler : IRequestHandler<RequestLoanCommand, Guid>
{
    private readonly IPayrollDbContext _context;

    public RequestLoanCommandHandler(IPayrollDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(RequestLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = new Loan(request.EmployeeId, request.LoanType, request.Amount,
            request.MonthlyDeduction, request.StartDate, request.TenantId);

        _context.Loans.Add(loan);
        await _context.SaveChangesAsync(cancellationToken);

        return loan.Id;
    }
}
