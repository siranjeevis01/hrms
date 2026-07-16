using HRMS.Services.Employee.Application.DTOs;
using MediatR;

namespace HRMS.Services.Employee.Application.Queries.GetEmployee;

public class GetEmployeeQuery : IRequest<EmployeeDto?>
{
    public Guid Id { get; set; }
}
