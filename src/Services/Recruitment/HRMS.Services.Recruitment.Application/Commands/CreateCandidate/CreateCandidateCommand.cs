using HRMS.Services.Recruitment.Domain.Enums;
using MediatR;

namespace HRMS.Services.Recruitment.Application.Commands.CreateCandidate;

public class CreateCandidateCommand : IRequest<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? CurrentCompany { get; set; }
    public string? CurrentDesignation { get; set; }
    public decimal? TotalExperience { get; set; }
    public decimal? ExpectedSalary { get; set; }
    public string Currency { get; set; } = "INR";
    public string? ResumeUrl { get; set; }
    public string? CoverLetter { get; set; }
    public CandidateSource Source { get; set; }
    public Guid? ReferralEmployeeId { get; set; }
    public string Skills { get; set; } = "[]";
    public string Education { get; set; } = "{}";
    public string? Notes { get; set; }
    public Guid TenantId { get; set; }
}
