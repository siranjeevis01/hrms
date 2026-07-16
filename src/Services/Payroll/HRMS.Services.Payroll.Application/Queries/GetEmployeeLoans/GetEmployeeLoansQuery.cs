using HRMS.Services.Payroll.Application.DTOs;
using MediatR;

namespace HRMS.Services.Payroll.Application.Queries.GetEmployeeLoans;

public class GetEmployeeLoansQuery : IRequest<List<LoanDto>>
{
    public Guid EmployeeId { get; set; }
}
