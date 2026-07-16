using MediatR;

namespace HRMS.Services.Performance.Application.Commands.CreateCalibrationSession;

public class CreateCalibrationSessionCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string ReviewPeriod { get; set; } = string.Empty;
    public Guid ConductedBy { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
