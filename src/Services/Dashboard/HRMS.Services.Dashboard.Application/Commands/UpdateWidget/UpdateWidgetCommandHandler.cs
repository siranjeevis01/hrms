using HRMS.Services.Dashboard.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Dashboard.Application.Commands.UpdateWidget;

public class UpdateWidgetCommandHandler : IRequestHandler<UpdateWidgetCommand>
{
    private readonly IDashboardDbContext _context;

    public UpdateWidgetCommandHandler(IDashboardDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateWidgetCommand request, CancellationToken cancellationToken)
    {
        var widget = await _context.DashboardWidgets
            .FirstOrDefaultAsync(w => w.Id == request.Id && w.DashboardId == request.DashboardId && w.TenantId == request.TenantId, cancellationToken);

        if (widget == null)
            throw new Exception($"Widget with ID {request.Id} not found.");

        widget.Update(
            request.WidgetType,
            request.Title,
            request.DataSource,
            request.Configuration,
            request.Position,
            request.Size,
            request.RefreshIntervalSeconds);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
