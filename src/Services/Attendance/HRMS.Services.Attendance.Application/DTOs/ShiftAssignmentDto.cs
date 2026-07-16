using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Attendance.Application.DTOs;

public class ShiftAssignmentDto : BaseDto
{
    public Guid EmployeeId { get; set; }
    public Guid ShiftId { get; set; }
    public DateTime EffectiveFrom { get; set; }
    public DateTime? EffectiveTo { get; set; }
    public bool IsCurrent { get; set; }
    public Guid TenantId { get; set; }
}
