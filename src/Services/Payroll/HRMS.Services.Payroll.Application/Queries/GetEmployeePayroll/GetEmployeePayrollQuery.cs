using HRMS.Services.Payroll.Application.DTOs;
using MediatR;

namespace HRMS.Services.Payroll.Application.Queries.GetEmployeePayroll;

public class GetEmployeePayrollQuery : IRequest<EmployeePayrollDto?>
{
    public Guid EmployeeId { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
}
