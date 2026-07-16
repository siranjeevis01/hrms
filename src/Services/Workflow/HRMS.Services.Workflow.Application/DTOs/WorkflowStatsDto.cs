using HRMS.Services.Workflow.Domain.Enums;

namespace HRMS.Services.Workflow.Application.DTOs;

public class WorkflowStatsDto
{
    public int TotalDefinitions { get; set; }
    public int ActiveDefinitions { get; set; }
    public int TotalInstances { get; set; }
    public int PendingInstances { get; set; }
    public int InProgressInstances { get; set; }
    public int ApprovedInstances { get; set; }
    public int RejectedInstances { get; set; }
    public int CancelledInstances { get; set; }
    public int ExpiredInstances { get; set; }
    public int TotalActions { get; set; }
    public int PendingApprovals { get; set; }
}
