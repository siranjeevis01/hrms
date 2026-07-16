using HRMS.Services.Recruitment.Application.DTOs;
using MediatR;

namespace HRMS.Services.Recruitment.Application.Queries.GetOnboardingChecklist;

public class GetOnboardingChecklistQuery : IRequest<List<OnboardingChecklistDto>>
{
    public Guid EmployeeId { get; set; }
}
