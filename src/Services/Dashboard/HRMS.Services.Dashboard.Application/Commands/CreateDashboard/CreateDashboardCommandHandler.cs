using HRMS.Services.Dashboard.Application.Interfaces;
using HRMS.Services.Dashboard.Domain.Entities;
using MediatR;

namespace HRMS.Services.Dashboard.Application.Commands.CreateDashboard;

public class CreateDashboardCommandHandler : IRequestHandler<CreateDashboardCommand, Guid>
{
    private readonly IDashboardDbContext _context;

    public CreateDashboardCommandHandler(IDashboardDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateDashboardCommand request, CancellationToken cancellationToken)
    {
        var dashboard = Domain.Entities.Dashboard.Create(
            request.Name,
            request.Description,
            request.UserId,
            request.IsDefault,
            request.IsPublic,
            request.Layout,
            request.TenantId);

        _context.Dashboards.Add(dashboard);
        await _context.SaveChangesAsync(cancellationToken);

        return dashboard.Id;
    }
}
