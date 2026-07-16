using HRMS.Services.ProjectTask.Domain.Enums;

namespace HRMS.Services.ProjectTask.Application.DTOs;

public class ProjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Code { get; set; } = string.Empty;
    public Guid DepartmentId { get; set; }
    public string? ClientName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ProjectStatus Status { get; set; }
    public ProjectPriority Priority { get; set; }
    public decimal Budget { get; set; }
    public decimal ActualCost { get; set; }
    public string? Currency { get; set; }
    public Guid? ProjectManagerId { get; set; }
    public Guid? OwnerId { get; set; }
    public decimal ProgressPercentage { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<ProjectMemberDto> Members { get; set; } = new();
}

public class ProjectListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public ProjectStatus Status { get; set; }
    public ProjectPriority Priority { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal ProgressPercentage { get; set; }
    public int MemberCount { get; set; }
}
