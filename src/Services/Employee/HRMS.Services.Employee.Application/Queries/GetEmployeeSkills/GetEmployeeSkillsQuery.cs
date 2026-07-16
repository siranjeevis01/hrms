using HRMS.Services.Employee.Application.DTOs;
using MediatR;

namespace HRMS.Services.Employee.Application.Queries.GetEmployeeSkills;

public class GetEmployeeSkillsQuery : IRequest<List<SkillDto>>
{
    public Guid EmployeeId { get; set; }
}
