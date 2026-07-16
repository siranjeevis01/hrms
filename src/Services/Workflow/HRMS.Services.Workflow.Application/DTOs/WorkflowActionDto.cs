using HRMS.Services.Workflow.Domain.Enums;

namespace HRMS.Services.Workflow.Application.DTOs;

public class WorkflowActionDto
{
    public Guid Id { get; set; }
    public Guid WorkflowInstanceId { get; set; }
    public Guid StepId { get; set; }
    public Guid ApproverId { get; set; }
    public WorkflowActionType Action { get; set; }
    public string? Comments { get; set; }
    public DateTime ActionedAt { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
