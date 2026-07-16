using HRMS.Services.Employee.Application.DTOs;
using HRMS.Services.Employee.Domain.Enums;
using MediatR;

namespace HRMS.Services.Employee.Application.Queries.GetEmployeesLeaving;

public class GetEmployeesLeavingQuery : IRequest<List<EmployeeListDto>>
{
    public Guid? DepartmentId { get; set; }
}
