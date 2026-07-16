using HRMS.Services.Employee.Domain.Enums;

namespace HRMS.Services.Employee.Domain.Entities;

public class EmployeeHistory : BaseEntity
{
    public Guid EmployeeId { get; private set; }
    public EmployeeAction Action { get; private set; }
    public DateTime ActionDate { get; private set; }
    public string? PreviousValue { get; private set; }
    public string? NewValue { get; private set; }
    public string? Reason { get; private set; }
    public Guid? ApprovedBy { get; private set; }
    public string TenantId { get; private set; } = string.Empty;

    private EmployeeHistory() { }

    public static EmployeeHistory Create(
        Guid employeeId, EmployeeAction action, DateTime actionDate,
        string? previousValue, string? newValue, string? reason,
        Guid? approvedBy, string tenantId)
    {
        return new EmployeeHistory
        {
            Id = Guid.NewGuid(),
            EmployeeId = employeeId,
            Action = action,
            ActionDate = actionDate,
            PreviousValue = previousValue,
            NewValue = newValue,
            Reason = reason,
            ApprovedBy = approvedBy,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
