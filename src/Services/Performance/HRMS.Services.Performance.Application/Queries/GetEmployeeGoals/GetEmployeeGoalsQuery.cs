using HRMS.Services.Performance.Application.DTOs;
using MediatR;

namespace HRMS.Services.Performance.Application.Queries.GetEmployeeGoals;

public class GetEmployeeGoalsQuery : IRequest<List<GoalDto>>
{
    public Guid EmployeeId { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
