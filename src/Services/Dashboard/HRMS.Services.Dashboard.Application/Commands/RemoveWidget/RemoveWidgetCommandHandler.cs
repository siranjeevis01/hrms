using HRMS.Services.Dashboard.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Dashboard.Application.Commands.RemoveWidget;

public class RemoveWidgetCommandHandler : IRequestHandler<RemoveWidgetCommand>
{
    private readonly IDashboardDbContext _context;

    public RemoveWidgetCommandHandler(IDashboardDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RemoveWidgetCommand request, CancellationToken cancellationToken)
    {
        var dashboard = await _context.Dashboards
            .FirstOrDefaultAsync(d => d.Id == request.DashboardId && d.TenantId == request.TenantId, cancellationToken);

        if (dashboard == null)
            throw new Exception($"Dashboard with ID {request.DashboardId} not found.");

        dashboard.RemoveWidget(request.Id);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
