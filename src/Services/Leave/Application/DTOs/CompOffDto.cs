using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Leave.Application.DTOs;

public class CompOffDto : BaseDto
{
    public Guid EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public Guid? LeaveApplicationId { get; set; }
    public DateTime EarnedDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public decimal Days { get; set; }
    public string? Reason { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime? UsedDate { get; set; }
}
