using HRMS.Services.Training.Application.Interfaces;
using HRMS.Services.Training.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Training.Application.Commands.SubmitAssessment;

public class SubmitAssessmentCommandHandler : IRequestHandler<SubmitAssessmentCommand, Guid>
{
    private readonly ITrainingDbContext _context;

    public SubmitAssessmentCommandHandler(ITrainingDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(SubmitAssessmentCommand request, CancellationToken cancellationToken)
    {
        var assessment = await _context.Assessments
            .FirstOrDefaultAsync(a => a.Id == request.AssessmentId, cancellationToken);

        if (assessment == null)
            throw new KeyNotFoundException($"Assessment with Id {request.AssessmentId} not found.");

        var attempt = AssessmentAttempt.Create(
            request.AssessmentId,
            request.EmployeeId,
            request.AttemptNumber,
            request.TotalPoints,
            request.TenantId);

        attempt.Submit(request.Answers, request.Score, assessment.PassingScore);

        _context.AssessmentAttempts.Add(attempt);
        await _context.SaveChangesAsync(cancellationToken);

        return attempt.Id;
    }
}
