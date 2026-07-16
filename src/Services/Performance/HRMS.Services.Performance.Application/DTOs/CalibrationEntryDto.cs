namespace HRMS.Services.Performance.Application.DTOs;

public class CalibrationEntryDto
{
    public Guid Id { get; set; }
    public Guid CalibrationSessionId { get; set; }
    public Guid EmployeeId { get; set; }
    public decimal OriginalRating { get; set; }
    public decimal CalibratedRating { get; set; }
    public string? Justification { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
