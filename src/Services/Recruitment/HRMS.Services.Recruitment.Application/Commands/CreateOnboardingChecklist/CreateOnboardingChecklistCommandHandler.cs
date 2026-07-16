using HRMS.Services.Recruitment.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Recruitment.Application.Commands.CreateOnboardingChecklist;

public class CreateOnboardingChecklistCommandHandler : IRequestHandler<CreateOnboardingChecklistCommand, Guid>
{
    private readonly IRecruitmentDbContext _context;

    public CreateOnboardingChecklistCommandHandler(IRecruitmentDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateOnboardingChecklistCommand request, CancellationToken cancellationToken)
    {
        var checklist = Domain.Entities.OnboardingChecklist.Create(
            request.EmployeeId,
            request.CandidateId,
            request.JoiningDate,
            request.Items,
            request.TenantId);

        _context.OnboardingChecklists.Add(checklist);
        await _context.SaveChangesAsync(cancellationToken);

        return checklist.Id;
    }
}
