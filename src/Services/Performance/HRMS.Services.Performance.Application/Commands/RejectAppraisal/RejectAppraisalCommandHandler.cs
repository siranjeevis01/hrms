using HRMS.Services.Performance.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.RejectAppraisal;

public class RejectAppraisalCommandHandler : IRequestHandler<RejectAppraisalCommand>
{
    private readonly IPerformanceDbContext _context;

    public RejectAppraisalCommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RejectAppraisalCommand request, CancellationToken cancellationToken)
    {
        var appraisal = await _context.Appraisals.FindAsync(new object[] { request.AppraisalId }, cancellationToken)
            ?? throw new Exception($"Appraisal with ID {request.AppraisalId} not found.");

        appraisal.Reject();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
