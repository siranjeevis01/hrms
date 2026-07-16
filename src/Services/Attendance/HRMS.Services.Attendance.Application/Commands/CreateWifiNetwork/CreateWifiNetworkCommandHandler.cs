using HRMS.Services.Attendance.Application.Interfaces;
using HRMS.Services.Attendance.Domain.Entities;
using MediatR;

namespace HRMS.Services.Attendance.Application.Commands.CreateWifiNetwork;

public class CreateWifiNetworkCommandHandler : IRequestHandler<CreateWifiNetworkCommand, Guid>
{
    private readonly IAttendanceDbContext _context;

    public CreateWifiNetworkCommandHandler(IAttendanceDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateWifiNetworkCommand request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId ?? Guid.Empty;

        var network = WifiNetwork.Create(
            request.CompanyId,
            request.BranchId,
            request.Name,
            request.Ssid,
            request.Bssid,
            request.IsPrimary,
            tenantId);

        _context.WifiNetworks.Add(network);
        await _context.SaveChangesAsync(cancellationToken);

        return network.Id;
    }
}
