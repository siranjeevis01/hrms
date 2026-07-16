using HRMS.Services.Employee.Application.DTOs;
using MediatR;

namespace HRMS.Services.Employee.Application.Queries.GetEmployeeSalary;

public class GetEmployeeSalaryQuery : IRequest<SalaryStructureDto?>
{
    public Guid EmployeeId { get; set; }
}
