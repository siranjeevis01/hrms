using MediatR;

namespace HRMS.Services.Attendance.Application.Commands.CreateGeoFence;

public class CreateGeoFenceCommand : IRequest<Guid>
{
    public Guid CompanyId { get; set; }
    public Guid? BranchId { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double RadiusMeters { get; set; }
    public string? WorkingDays { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public Guid? TenantId { get; set; }
}
