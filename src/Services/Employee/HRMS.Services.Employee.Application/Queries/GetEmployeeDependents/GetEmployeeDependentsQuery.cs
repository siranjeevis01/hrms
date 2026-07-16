using HRMS.Services.Employee.Application.DTOs;
using MediatR;

namespace HRMS.Services.Employee.Application.Queries.GetEmployeeDependents;

public class GetEmployeeDependentsQuery : IRequest<List<DependentDto>>
{
    public Guid EmployeeId { get; set; }
}
