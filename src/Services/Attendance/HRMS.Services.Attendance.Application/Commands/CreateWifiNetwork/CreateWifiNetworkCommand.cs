using MediatR;

namespace HRMS.Services.Attendance.Application.Commands.CreateWifiNetwork;

public class CreateWifiNetworkCommand : IRequest<Guid>
{
    public Guid CompanyId { get; set; }
    public Guid? BranchId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Ssid { get; set; } = string.Empty;
    public string Bssid { get; set; } = string.Empty;
    public bool IsPrimary { get; set; }
    public Guid? TenantId { get; set; }
}
