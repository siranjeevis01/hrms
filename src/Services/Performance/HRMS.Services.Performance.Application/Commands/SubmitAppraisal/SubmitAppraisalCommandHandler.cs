using HRMS.Services.Performance.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.SubmitAppraisal;

public class SubmitAppraisalCommandHandler : IRequestHandler<SubmitAppraisalCommand>
{
    private readonly IPerformanceDbContext _context;

    public SubmitAppraisalCommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task Handle(SubmitAppraisalCommand request, CancellationToken cancellationToken)
    {
        var appraisal = await _context.Appraisals.FindAsync(new object[] { request.AppraisalId }, cancellationToken)
            ?? throw new Exception($"Appraisal with ID {request.AppraisalId} not found.");

        appraisal.Submit();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
