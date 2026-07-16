using HRMS.Services.Payroll.Application.DTOs;
using MediatR;

namespace HRMS.Services.Payroll.Application.Queries.GetEmployeeSalaryBreakdown;

public class GetEmployeeSalaryBreakdownQuery : IRequest<SalaryBreakdownDto?>
{
    public Guid EmployeeId { get; set; }
    public Guid TenantId { get; set; }
}
