using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Leave.Application.DTOs;

public class LeaveBalanceDto : BaseDto
{
    public Guid EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public Guid LeaveTypeId { get; set; }
    public string? LeaveTypeName { get; set; }
    public string? LeaveTypeCode { get; set; }
    public int Year { get; set; }
    public decimal TotalDays { get; set; }
    public decimal UsedDays { get; set; }
    public decimal PendingDays { get; set; }
    public decimal CarryForwardDays { get; set; }
    public decimal EncashedDays { get; set; }
    public decimal AdjustedDays { get; set; }
    public decimal AvailableDays { get; set; }
    public DateTime? LastAccrualDate { get; set; }
}
