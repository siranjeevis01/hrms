using MediatR;

namespace HRMS.Services.Payroll.Application.Commands.ApproveLoan;

public class ApproveLoanCommand : IRequest
{
    public Guid LoanId { get; set; }
    public Guid ApprovedBy { get; set; }
}
