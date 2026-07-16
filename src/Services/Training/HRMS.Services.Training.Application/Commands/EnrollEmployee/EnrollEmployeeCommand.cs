using MediatR;

namespace HRMS.Services.Training.Application.Commands.EnrollEmployee;

public class EnrollEmployeeCommand : IRequest<Guid>
{
    public Guid CourseId { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid TenantId { get; set; }
}
