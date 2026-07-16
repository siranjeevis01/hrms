using HRMS.Services.Recruitment.Application.Interfaces;
using HRMS.Services.Recruitment.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Recruitment.Application.Commands.CompleteOnboardingItem;

public class CompleteOnboardingItemCommandHandler : IRequestHandler<CompleteOnboardingItemCommand, Unit>
{
    private readonly IRecruitmentDbContext _context;

    public CompleteOnboardingItemCommandHandler(IRecruitmentDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(CompleteOnboardingItemCommand request, CancellationToken cancellationToken)
    {
        var checklist = await _context.OnboardingChecklists
            .FirstOrDefaultAsync(o => o.Id == request.OnboardingChecklistId, cancellationToken)
            ?? throw new InvalidOperationException($"Onboarding checklist with ID {request.OnboardingChecklistId} not found.");

        checklist.UpdateItems(request.UpdatedItems);
        checklist.UpdateStatus(OnboardingStatus.InProgress);

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
