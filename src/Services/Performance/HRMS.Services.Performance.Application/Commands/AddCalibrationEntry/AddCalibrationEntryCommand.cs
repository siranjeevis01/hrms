using MediatR;

namespace HRMS.Services.Performance.Application.Commands.AddCalibrationEntry;

public class AddCalibrationEntryCommand : IRequest<Guid>
{
    public Guid CalibrationSessionId { get; set; }
    public Guid EmployeeId { get; set; }
    public decimal OriginalRating { get; set; }
    public decimal CalibratedRating { get; set; }
    public string? Justification { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
