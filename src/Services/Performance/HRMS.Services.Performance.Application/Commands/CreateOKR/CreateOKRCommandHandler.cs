using HRMS.Services.Performance.Application.Interfaces;
using HRMS.Services.Performance.Domain.Entities;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.CreateOKR;

public class CreateOKRCommandHandler : IRequestHandler<CreateOKRCommand, Guid>
{
    private readonly IPerformanceDbContext _context;

    public CreateOKRCommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateOKRCommand request, CancellationToken cancellationToken)
    {
        var okr = OKR.Create(
            request.EmployeeId,
            request.ManagerId,
            request.Period,
            request.TenantId);

        _context.OKRs.Add(okr);
        await _context.SaveChangesAsync(cancellationToken);

        return okr.Id;
    }
}
