using HRMS.Services.Dashboard.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Dashboard.Application.Commands.UpdateDashboard;

public class UpdateDashboardCommandHandler : IRequestHandler<UpdateDashboardCommand>
{
    private readonly IDashboardDbContext _context;

    public UpdateDashboardCommandHandler(IDashboardDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateDashboardCommand request, CancellationToken cancellationToken)
    {
        var dashboard = await _context.Dashboards
            .FirstOrDefaultAsync(d => d.Id == request.Id && d.TenantId == request.TenantId, cancellationToken);

        if (dashboard == null)
            throw new Exception($"Dashboard with ID {request.Id} not found.");

        dashboard.Update(
            request.Name,
            request.Description,
            request.IsDefault,
            request.IsPublic,
            request.Layout);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
