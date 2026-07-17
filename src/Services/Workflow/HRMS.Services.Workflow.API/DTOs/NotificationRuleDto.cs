namespace HRMS.Services.Workflow.API.DTOs;

public class NotificationRuleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string EventType { get; set; } = string.Empty;
    public string Channel { get; set; } = string.Empty;
    public string? RecipientRole { get; set; }
    public string? Template { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class PendingApprovalDto
{
    public Guid Id { get; set; }
    public string WorkflowName { get; set; } = string.Empty;
    public string? RequesterName { get; set; }
    public string? Description { get; set; }
    public string Priority { get; set; } = "Medium";
    public DateTime CreatedAt { get; set; }
}

public class WorkflowStatsDto
{
    public int TotalWorkflows { get; set; }
    public int ActiveWorkflows { get; set; }
    public int PendingApprovals { get; set; }
    public int CompletedThisMonth { get; set; }
    public int AverageCompletionDays { get; set; }
}
