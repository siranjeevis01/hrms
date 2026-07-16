using HRMS.Services.Employee.Domain.Enums;
using MediatR;

namespace HRMS.Services.Employee.Application.Commands.ChangeEmployeeStatus;

public class ChangeEmployeeStatusCommand : IRequest<Unit>
{
    public Guid EmployeeId { get; set; }
    public EmploymentStatus NewStatus { get; set; }
    public string? Reason { get; set; }
}
