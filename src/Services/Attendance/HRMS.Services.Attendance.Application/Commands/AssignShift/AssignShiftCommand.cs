using MediatR;

namespace HRMS.Services.Attendance.Application.Commands.AssignShift;

public class AssignShiftCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public Guid ShiftId { get; set; }
    public DateTime EffectiveFrom { get; set; }
    public DateTime? EffectiveTo { get; set; }
    public Guid? TenantId { get; set; }
}
