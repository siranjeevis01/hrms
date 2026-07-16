using MediatR;

namespace HRMS.Services.Attendance.Application.Queries.ValidateWifi;

public class ValidateWifiQuery : IRequest<ValidateWifiResult>
{
    public string Ssid { get; set; } = string.Empty;
    public string? Bssid { get; set; }
    public Guid CompanyId { get; set; }
}

public class ValidateWifiResult
{
    public bool IsValid { get; set; }
    public string? Message { get; set; }
    public string? NetworkName { get; set; }
}
