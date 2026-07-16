using HRMS.Services.Payroll.Application.Interfaces;
using HRMS.Services.Payroll.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Payroll.Application.Commands.ProcessLoanDeduction;

public class ProcessLoanDeductionCommandHandler : IRequestHandler<ProcessLoanDeductionCommand, List<LoanDeductionResult>>
{
    private readonly IPayrollDbContext _context;

    public ProcessLoanDeductionCommandHandler(IPayrollDbContext context)
    {
        _context = context;
    }

    public async Task<List<LoanDeductionResult>> Handle(ProcessLoanDeductionCommand request, CancellationToken cancellationToken)
    {
        var results = new List<LoanDeductionResult>();

        var activeLoans = await _context.Loans
            .Where(l => l.Status == LoanStatus.Active && l.TenantId == request.TenantId)
            .ToListAsync(cancellationToken);

        var employeePayrolls = await _context.EmployeePayrolls
            .Where(ep => ep.PayrollRunId == request.PayrollRunId)
            .ToListAsync(cancellationToken);

        foreach (var loan in activeLoans)
        {
            var employeePayroll = employeePayrolls.FirstOrDefault(ep => ep.EmployeeId == loan.EmployeeId);
            if (employeePayroll == null) continue;

            var deductionAmount = Math.Min(loan.MonthlyDeduction, loan.OutstandingAmount);
            loan.DeductPayment(deductionAmount, employeePayroll.Id);

            results.Add(new LoanDeductionResult
            {
                LoanId = loan.Id,
                EmployeeId = loan.EmployeeId,
                DeductedAmount = deductionAmount,
                RemainingBalance = loan.OutstandingAmount
            });
        }

        await _context.SaveChangesAsync(cancellationToken);
        return results;
    }
}
