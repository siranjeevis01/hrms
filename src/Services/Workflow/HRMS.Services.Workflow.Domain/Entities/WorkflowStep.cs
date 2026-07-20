using HRMS.Services.Workflow.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Workflow.Domain.Entities;

public class WorkflowStep : BaseEntity
{
    public Guid WorkflowDefinitionId { get; private set; }
    public int StepNumber { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public ApproverType ApproverType { get; private set; }
    public Guid? ApproverId { get; private set; }
    public WorkflowActionType Action { get; private set; }
    public int? TimeoutHours { get; private set; }
    public bool IsRequired { get; private set; }
    public new string TenantId { get; private set; } = string.Empty;

    private WorkflowStep() { }

    public static WorkflowStep Create(
        Guid workflowDefinitionId,
        int stepNumber,
        string name,
        ApproverType approverType,
        Guid? approverId,
        WorkflowActionType action,
        int? timeoutHours,
        bool isRequired,
        string tenantId)
    {
        return new WorkflowStep
        {
            Id = Guid.NewGuid(),
            WorkflowDefinitionId = workflowDefinitionId,
            StepNumber = stepNumber,
            Name = name,
            ApproverType = approverType,
            ApproverId = approverId,
            Action = action,
            TimeoutHours = timeoutHours,
            IsRequired = isRequired,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        string? name,
        ApproverType? approverType,
        Guid? approverId,
        WorkflowActionType? action,
        int? timeoutHours,
        bool? isRequired)
    {
        Name = name ?? Name;
        if (approverType.HasValue) ApproverType = approverType.Value;
        if (approverId.HasValue) ApproverId = approverId;
        if (action.HasValue) Action = action.Value;
        if (timeoutHours.HasValue) TimeoutHours = timeoutHours;
        if (isRequired.HasValue) IsRequired = isRequired.Value;
        UpdatedAt = DateTime.UtcNow;
    }
}
