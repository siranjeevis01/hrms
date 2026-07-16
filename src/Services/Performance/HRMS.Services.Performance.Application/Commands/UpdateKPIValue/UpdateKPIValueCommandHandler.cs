using HRMS.Services.Performance.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.UpdateKPIValue;

public class UpdateKPIValueCommandHandler : IRequestHandler<UpdateKPIValueCommand>
{
    private readonly IPerformanceDbContext _context;

    public UpdateKPIValueCommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateKPIValueCommand request, CancellationToken cancellationToken)
    {
        var kpi = await _context.KPIs.FindAsync(new object[] { request.KPIId }, cancellationToken)
            ?? throw new Exception($"KPI with ID {request.KPIId} not found.");

        kpi.UpdateValue(request.CurrentValue);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
