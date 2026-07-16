using HRMS.Services.Performance.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.UpdateKPI;

public class UpdateKPICommandHandler : IRequestHandler<UpdateKPICommand>
{
    private readonly IPerformanceDbContext _context;

    public UpdateKPICommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateKPICommand request, CancellationToken cancellationToken)
    {
        var kpi = await _context.KPIs.FindAsync(new object[] { request.Id }, cancellationToken)
            ?? throw new Exception($"KPI with ID {request.Id} not found.");

        kpi.Update(request.Name, request.Description, request.Category, request.TargetValue, request.Unit, request.Weight, request.ScoringMethod, request.Period);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
