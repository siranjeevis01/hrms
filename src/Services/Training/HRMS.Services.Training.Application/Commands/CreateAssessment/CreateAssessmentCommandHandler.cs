using HRMS.Services.Training.Application.Interfaces;
using HRMS.Services.Training.Domain.Entities;
using MediatR;

namespace HRMS.Services.Training.Application.Commands.CreateAssessment;

public class CreateAssessmentCommandHandler : IRequestHandler<CreateAssessmentCommand, Guid>
{
    private readonly ITrainingDbContext _context;

    public CreateAssessmentCommandHandler(ITrainingDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateAssessmentCommand request, CancellationToken cancellationToken)
    {
        var assessment = Assessment.Create(
            request.CourseId,
            request.Title,
            request.Description,
            request.PassingScore,
            request.TotalPoints,
            request.TimeLimitMinutes,
            request.MaxAttempts,
            request.TenantId);

        _context.Assessments.Add(assessment);
        await _context.SaveChangesAsync(cancellationToken);

        return assessment.Id;
    }
}
