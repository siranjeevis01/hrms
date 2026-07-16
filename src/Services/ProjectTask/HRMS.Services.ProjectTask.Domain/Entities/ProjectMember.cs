using HRMS.Services.ProjectTask.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.ProjectTask.Domain.Entities;

public class ProjectMember : BaseEntity
{
    public Guid ProjectId { get; private set; }
    public Guid EmployeeId { get; private set; }
    public ProjectMemberRole Role { get; private set; }
    public decimal AllocationPercentage { get; private set; }
    public DateTime JoinedAt { get; private set; }
    public DateTime? LeftAt { get; private set; }

    private ProjectMember() { }

    public static ProjectMember Create(
        Guid projectId,
        Guid employeeId,
        ProjectMemberRole role,
        decimal allocationPercentage,
        Guid tenantId)
    {
        return new ProjectMember
        {
            Id = Guid.NewGuid(),
            ProjectId = projectId,
            EmployeeId = employeeId,
            Role = role,
            AllocationPercentage = allocationPercentage,
            JoinedAt = DateTime.UtcNow,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow
        };
    }
}
