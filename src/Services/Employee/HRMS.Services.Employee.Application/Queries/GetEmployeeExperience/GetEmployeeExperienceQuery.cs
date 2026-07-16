using HRMS.Services.Employee.Application.DTOs;
using MediatR;

namespace HRMS.Services.Employee.Application.Queries.GetEmployeeExperience;

public class GetEmployeeExperienceQuery : IRequest<List<WorkExperienceDto>>
{
    public Guid EmployeeId { get; set; }
}
