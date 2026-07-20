using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Payroll.Domain.Entities;

public class PayrollAuditLog : BaseEntity
{
    public new Guid Id { get; private set; }
    public Guid PayrollRunId { get; private set; }
    public string Action { get; private set; } = string.Empty;
    public string PerformedBy { get; private set; } = string.Empty;
    public DateTime PerformedAt { get; private set; }
    public string? OldValue { get; private set; }
    public string? NewValue { get; private set; }
    public string? Details { get; private set; }

    public PayrollRun PayrollRun { get; private set; } = null!;

    private PayrollAuditLog() { }

    public PayrollAuditLog(Guid payrollRunId, string action, string performedBy, string? oldValue, string? newValue)
    {
        Id = Guid.NewGuid();
        PayrollRunId = payrollRunId;
        Action = action;
        PerformedBy = performedBy;
        PerformedAt = DateTime.UtcNow;
        OldValue = oldValue;
        NewValue = newValue;
        CreatedAt = DateTime.UtcNow;
    }
}
