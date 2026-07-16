using HRMS.Services.Performance.Application.Events;
using HRMS.Services.Performance.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.ApproveAppraisal;

public class ApproveAppraisalCommandHandler : IRequestHandler<ApproveAppraisalCommand>
{
    private readonly IPerformanceDbContext _context;

    public ApproveAppraisalCommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ApproveAppraisalCommand request, CancellationToken cancellationToken)
    {
        var appraisal = await _context.Appraisals.FindAsync(new object[] { request.AppraisalId }, cancellationToken)
            ?? throw new Exception($"Appraisal with ID {request.AppraisalId} not found.");

        appraisal.Approve(
            request.ApprovedBy,
            request.FinalRating,
            request.HikePercentage,
            request.PromotionRecommended,
            request.Bonus);

        appraisal.RaiseEvent(new AppraisalApprovedEvent(appraisal.Id, appraisal.EmployeeId, appraisal.FinalRating, appraisal.HikePercentage));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
