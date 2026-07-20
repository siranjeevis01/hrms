using HRMS.Services.Workflow.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Workflow.Domain.Entities;

public class WorkflowAction : BaseEntity
{
    public Guid WorkflowInstanceId { get; private set; }
    public Guid StepId { get; private set; }
    public Guid ApproverId { get; private set; }
    public WorkflowActionType Action { get; private set; }
    public string? Comments { get; private set; }
    public DateTime ActionedAt { get; private set; }
    public new string TenantId { get; private set; } = string.Empty;

    private WorkflowAction() { }

    public static WorkflowAction Create(
        Guid workflowInstanceId,
        Guid stepId,
        Guid approverId,
        WorkflowActionType action,
        string? comments,
        string tenantId)
    {
        return new WorkflowAction
        {
            Id = Guid.NewGuid(),
            WorkflowInstanceId = workflowInstanceId,
            StepId = stepId,
            ApproverId = approverId,
            Action = action,
            Comments = comments,
            ActionedAt = DateTime.UtcNow,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void UpdateComments(string? comments)
    {
        Comments = comments;
        UpdatedAt = DateTime.UtcNow;
    }
}
