using HRMS.Services.Performance.Application.Interfaces;
using HRMS.Services.Performance.Domain.Entities;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.CreateKPI;

public class CreateKPICommandHandler : IRequestHandler<CreateKPICommand, Guid>
{
    private readonly IPerformanceDbContext _context;

    public CreateKPICommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateKPICommand request, CancellationToken cancellationToken)
    {
        var kpi = KPI.Create(
            request.Name,
            request.Description,
            request.Category,
            request.EmployeeId,
            request.DepartmentId,
            request.MetricType,
            request.TargetValue,
            request.Unit,
            request.Weight,
            request.ScoringMethod,
            request.Period,
            request.TenantId);

        _context.KPIs.Add(kpi);
        await _context.SaveChangesAsync(cancellationToken);

        return kpi.Id;
    }
}
