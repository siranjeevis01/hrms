using HRMS.Services.Workflow.Domain.Enums;

namespace HRMS.Services.Workflow.Application.DTOs;

public class PendingApprovalDto
{
    public Guid InstanceId { get; set; }
    public Guid WorkflowDefinitionId { get; set; }
    public string WorkflowDefinitionName { get; set; } = string.Empty;
    public WorkflowEntityType EntityType { get; set; }
    public Guid EntityId { get; set; }
    public Guid RequestedById { get; set; }
    public int CurrentStepNumber { get; set; }
    public string CurrentStepName { get; set; } = string.Empty;
    public Guid CurrentStepId { get; set; }
    public WorkflowStatus Status { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
