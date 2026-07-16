using HRMS.Services.ProjectTask.Domain.Enums;
using MediatR;

namespace HRMS.Services.ProjectTask.Application.Commands.AddProjectMember;

public class AddProjectMemberCommand : IRequest<Guid>
{
    public Guid ProjectId { get; set; }
    public Guid EmployeeId { get; set; }
    public ProjectMemberRole Role { get; set; }
    public decimal AllocationPercentage { get; set; }
    public Guid TenantId { get; set; }
}
