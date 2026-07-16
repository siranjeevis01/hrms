using HRMS.Services.Dashboard.Application.Interfaces;
using HRMS.Services.Dashboard.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Dashboard.Application.Commands.ShareDashboard;

public class ShareDashboardCommandHandler : IRequestHandler<ShareDashboardCommand, Guid>
{
    private readonly IDashboardDbContext _context;

    public ShareDashboardCommandHandler(IDashboardDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(ShareDashboardCommand request, CancellationToken cancellationToken)
    {
        var dashboard = await _context.Dashboards
            .FirstOrDefaultAsync(d => d.Id == request.DashboardId && d.TenantId == request.TenantId, cancellationToken);

        if (dashboard == null)
            throw new Exception($"Dashboard with ID {request.DashboardId} not found.");

        var share = DashboardShare.Create(
            request.DashboardId,
            request.SharedWithUserId,
            request.Permission,
            request.TenantId);

        dashboard.AddShare(share);
        await _context.SaveChangesAsync(cancellationToken);

        return share.Id;
    }
}
