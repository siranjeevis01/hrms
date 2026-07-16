using HRMS.Services.Employee.Application.DTOs;
using MediatR;

namespace HRMS.Services.Employee.Application.Queries.GetEmployeeEducation;

public class GetEmployeeEducationQuery : IRequest<List<EducationDto>>
{
    public Guid EmployeeId { get; set; }
}
