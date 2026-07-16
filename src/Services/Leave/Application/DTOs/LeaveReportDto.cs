using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Leave.Application.DTOs;

public class LeaveReportDto : BaseDto
{
    public string? Department { get; set; }
    public string? DepartmentName { get; set; }
    public int EmployeeCount { get; set; }
    public decimal TotalLeavesTaken { get; set; }
    public decimal AverageLeavesPerEmployee { get; set; }
    public Dictionary<string, decimal> LeaveTypeBreakdown { get; set; } = new();
    public int PendingCount { get; set; }
    public int ApprovedCount { get; set; }
    public int RejectedCount { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}
