using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Attendance.Domain.Entities;

public class ShiftAssignment : BaseEntity
{
    public Guid EmployeeId { get; private set; }
    public Guid ShiftId { get; private set; }
    public DateTime EffectiveFrom { get; private set; }
    public DateTime? EffectiveTo { get; private set; }
    public bool IsCurrent { get; private set; }

    private ShiftAssignment() { }

    public static ShiftAssignment Create(Guid employeeId, Guid shiftId, DateTime effectiveFrom, DateTime? effectiveTo = null, Guid? tenantId = null)
    {
        var assignment = new ShiftAssignment
        {
            EmployeeId = employeeId,
            ShiftId = shiftId,
            EffectiveFrom = effectiveFrom.Date,
            EffectiveTo = effectiveTo?.Date,
            IsCurrent = true
        };

        if (tenantId.HasValue)
            assignment.TenantId = tenantId.Value;

        return assignment;
    }

    public void EndAssignment(DateTime endDate)
    {
        EffectiveTo = endDate.Date;
        IsCurrent = false;
    }

    public void MakeCurrent()
    {
        IsCurrent = true;
    }
}
