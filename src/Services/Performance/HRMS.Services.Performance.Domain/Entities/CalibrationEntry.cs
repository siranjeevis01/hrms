using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Performance.Domain.Entities;

public class CalibrationEntry : BaseEntity
{
    public Guid CalibrationSessionId { get; private set; }
    public Guid EmployeeId { get; private set; }
    public decimal OriginalRating { get; private set; }
    public decimal CalibratedRating { get; private set; }
    public string? Justification { get; private set; }
    public new string TenantId { get; private set; } = string.Empty;

    private CalibrationEntry() { }

    public static CalibrationEntry Create(
        Guid calibrationSessionId,
        Guid employeeId,
        decimal originalRating,
        decimal calibratedRating,
        string? justification,
        string tenantId)
    {
        return new CalibrationEntry
        {
            Id = Guid.NewGuid(),
            CalibrationSessionId = calibrationSessionId,
            EmployeeId = employeeId,
            OriginalRating = originalRating,
            CalibratedRating = calibratedRating,
            Justification = justification,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void UpdateCalibration(decimal calibratedRating, string? justification)
    {
        CalibratedRating = calibratedRating;
        Justification = justification ?? Justification;
        UpdatedAt = DateTime.UtcNow;
    }
}
