using HRMS.Services.Performance.Domain.Enums;

namespace HRMS.Services.Performance.Application.DTOs;

public class CalibrationSessionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string ReviewPeriod { get; set; } = string.Empty;
    public Guid ConductedBy { get; set; }
    public CalibrationStatus Status { get; set; }
    public string TenantId { get; set; } = string.Empty;
    public List<CalibrationEntryDto> Entries { get; set; } = new();
}
