using HRMS.Services.Performance.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Performance.Application.Commands.SubmitSelfAssessment;

public class SubmitSelfAssessmentCommandHandler : IRequestHandler<SubmitSelfAssessmentCommand>
{
    private readonly IPerformanceDbContext _context;

    public SubmitSelfAssessmentCommandHandler(IPerformanceDbContext context)
    {
        _context = context;
    }

    public async Task Handle(SubmitSelfAssessmentCommand request, CancellationToken cancellationToken)
    {
        var appraisal = await _context.Appraisals.FindAsync(new object[] { request.AppraisalId }, cancellationToken)
            ?? throw new Exception($"Appraisal with ID {request.AppraisalId} not found.");

        appraisal.SubmitSelfAssessment(request.SelfRating, request.Achievements, request.Goals);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
