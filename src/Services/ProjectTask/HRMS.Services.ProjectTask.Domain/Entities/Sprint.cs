using HRMS.Services.ProjectTask.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.ProjectTask.Domain.Entities;

public class Sprint : BaseEntity
{
    public Guid ProjectId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Goal { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public new SprintStatus Status { get; private set; }

    private Sprint() { }

    public static Sprint Create(
        Guid projectId,
        string name,
        string? goal,
        DateTime startDate,
        DateTime endDate,
        Guid tenantId)
    {
        return new Sprint
        {
            Id = Guid.NewGuid(),
            ProjectId = projectId,
            Name = name,
            Goal = goal,
            StartDate = startDate,
            EndDate = endDate,
            Status = SprintStatus.Planning,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Start()
    {
        Status = SprintStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        Status = SprintStatus.Completed;
        UpdatedAt = DateTime.UtcNow;
    }
}
