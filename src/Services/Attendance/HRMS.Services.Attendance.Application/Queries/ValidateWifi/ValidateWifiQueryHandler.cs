using HRMS.Services.Attendance.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Attendance.Application.Queries.ValidateWifi;

public class ValidateWifiQueryHandler : IRequestHandler<ValidateWifiQuery, ValidateWifiResult>
{
    private readonly IAttendanceDbContext _context;

    public ValidateWifiQueryHandler(IAttendanceDbContext context)
    {
        _context = context;
    }

    public async Task<ValidateWifiResult> Handle(ValidateWifiQuery request, CancellationToken cancellationToken)
    {
        var networks = await _context.WifiNetworks
            .Where(w => w.CompanyId == request.CompanyId && w.IsActive)
            .ToListAsync(cancellationToken);

        if (!networks.Any())
        {
            return new ValidateWifiResult
            {
                IsValid = false,
                Message = "No active WiFi networks configured for this company."
            };
        }

        foreach (var network in networks)
        {
            if (network.Matches(request.Ssid, request.Bssid))
            {
                return new ValidateWifiResult
                {
                    IsValid = true,
                    Message = $"Connected to authorized network: {network.Name}",
                    NetworkName = network.Name
                };
            }
        }

        return new ValidateWifiResult
        {
            IsValid = false,
            Message = "Connected WiFi network is not authorized for attendance."
        };
    }
}
