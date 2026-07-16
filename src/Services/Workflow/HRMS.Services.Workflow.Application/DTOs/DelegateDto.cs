using HRMS.Services.Workflow.Domain.Enums;

namespace HRMS.Services.Workflow.Application.DTOs;

public class DelegateDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid DelegateToUserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public WorkflowEntityType? EntityType { get; set; }
    public string TenantId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
