using HRMS.Services.Dashboard.Application.Interfaces;
using HRMS.Services.Dashboard.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Dashboard.Application.Commands.AddWidget;

public class AddWidgetCommandHandler : IRequestHandler<AddWidgetCommand, Guid>
{
    private readonly IDashboardDbContext _context;

    public AddWidgetCommandHandler(IDashboardDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddWidgetCommand request, CancellationToken cancellationToken)
    {
        var dashboard = await _context.Dashboards
            .FirstOrDefaultAsync(d => d.Id == request.DashboardId && d.TenantId == request.TenantId, cancellationToken);

        if (dashboard == null)
            throw new Exception($"Dashboard with ID {request.DashboardId} not found.");

        var widget = DashboardWidget.Create(
            request.DashboardId,
            request.WidgetType,
            request.Title,
            request.DataSource,
            request.Configuration,
            request.Position,
            request.Size,
            request.RefreshIntervalSeconds,
            request.TenantId);

        dashboard.AddWidget(widget);
        await _context.SaveChangesAsync(cancellationToken);

        return widget.Id;
    }
}
