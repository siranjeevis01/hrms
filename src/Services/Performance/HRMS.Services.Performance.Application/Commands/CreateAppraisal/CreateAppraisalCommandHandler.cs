using HRMS.Services.Performance.Application.Interfaces;
using HRMS.Services.Performance.Domain.Entities;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.CreateAppraisal;

public class CreateAppraisalCommandHandler : IRequestHandler<CreateAppraisalCommand, Guid>
{
    private readonly IPerformanceDbContext _context;

    public CreateAppraisalCommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateAppraisalCommand request, CancellationToken cancellationToken)
    {
        var appraisal = Appraisal.Create(
            request.EmployeeId,
            request.ManagerId,
            request.Period,
            request.Type,
            request.Comments,
            request.TenantId);

        _context.Appraisals.Add(appraisal);
        await _context.SaveChangesAsync(cancellationToken);

        return appraisal.Id;
    }
}
