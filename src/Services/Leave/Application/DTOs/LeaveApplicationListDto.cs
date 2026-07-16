using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Leave.Application.DTOs;

public class LeaveApplicationListDto : BaseDto
{
    public Guid EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public string? EmployeeCode { get; set; }
    public Guid LeaveTypeId { get; set; }
    public string? LeaveTypeName { get; set; }
    public string? LeaveTypeColor { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalDays { get; set; }
    public bool IsHalfDay { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime AppliedAt { get; set; }
    public int? CurrentApprovalLevel { get; set; }
}
