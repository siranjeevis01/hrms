using HRMS.Services.Payroll.Domain.Enums;
using MediatR;

namespace HRMS.Services.Payroll.Application.Commands.RequestLoan;

public class RequestLoanCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public LoanType LoanType { get; set; }
    public decimal Amount { get; set; }
    public decimal MonthlyDeduction { get; set; }
    public DateOnly StartDate { get; set; }
    public Guid TenantId { get; set; }
}
