using MediatR;

namespace HRMS.Services.Recruitment.Application.Commands.CreateOnboardingChecklist;

public class CreateOnboardingChecklistCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public Guid CandidateId { get; set; }
    public DateTime JoiningDate { get; set; }
    public string Items { get; set; } = "[]";
    public Guid TenantId { get; set; }
}
