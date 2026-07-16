using HRMS.Services.Payroll.Application.DTOs;
using MediatR;

namespace HRMS.Services.Payroll.Application.Queries.GetLoanDetails;

public class GetLoanDetailsQuery : IRequest<LoanDto?>
{
    public Guid LoanId { get; set; }
}
