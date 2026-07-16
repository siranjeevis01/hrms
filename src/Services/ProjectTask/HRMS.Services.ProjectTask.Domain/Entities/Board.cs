using HRMS.Services.ProjectTask.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.ProjectTask.Domain.Entities;

public class Board : BaseEntity
{
    public Guid ProjectId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public BoardType Type { get; private set; }
    public string? Columns { get; private set; }

    private Board() { }

    public static Board Create(
        Guid projectId,
        string name,
        BoardType type,
        string? columns,
        Guid tenantId)
    {
        return new Board
        {
            Id = Guid.NewGuid(),
            ProjectId = projectId,
            Name = name,
            Type = type,
            Columns = columns,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow
        };
    }
}
