using MediatR;

namespace HRMS.Services.Recruitment.Application.Commands.CompleteOnboardingItem;

public class CompleteOnboardingItemCommand : IRequest<Unit>
{
    public Guid OnboardingChecklistId { get; set; }
    public string UpdatedItems { get; set; } = "[]";
}
