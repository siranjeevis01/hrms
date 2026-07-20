using HRMS.Shared.Kernel.Common;
using HRMS.Services.Recruitment.Domain.Enums;

namespace HRMS.Services.Recruitment.Domain.Entities;

public class Candidate : AggregateRoot
{
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public string? CurrentCompany { get; private set; }
    public string? CurrentDesignation { get; private set; }
    public decimal? TotalExperience { get; private set; }
    public decimal? ExpectedSalary { get; private set; }
    public string Currency { get; private set; } = "INR";
    public string? ResumeUrl { get; private set; }
    public string? CoverLetter { get; private set; }
    public CandidateSource Source { get; private set; }
    public Guid? ReferralEmployeeId { get; private set; }
    public string Skills { get; private set; } = "[]";
    public string Education { get; private set; } = "{}";
    public new CandidateStatus Status { get; private set; }
    public string? RejectionReason { get; private set; }
    public string? Notes { get; private set; }
    public new Guid TenantId { get; private set; }

    private Candidate() { }

    public static Candidate Create(
        string firstName, string lastName, string email, string phoneNumber,
        string? currentCompany, string? currentDesignation, decimal? totalExperience,
        decimal? expectedSalary, string currency, string? resumeUrl, string? coverLetter,
        CandidateSource source, Guid? referralEmployeeId, string skills, string education,
        Guid tenantId)
    {
        return new Candidate
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber,
            CurrentCompany = currentCompany,
            CurrentDesignation = currentDesignation,
            TotalExperience = totalExperience,
            ExpectedSalary = expectedSalary,
            Currency = currency,
            ResumeUrl = resumeUrl,
            CoverLetter = coverLetter,
            Source = source,
            ReferralEmployeeId = referralEmployeeId,
            Skills = skills,
            Education = education,
            Status = CandidateStatus.New,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        string firstName, string lastName, string email, string phoneNumber,
        string? currentCompany, string? currentDesignation, decimal? totalExperience,
        decimal? expectedSalary, string currency, string? resumeUrl, string? coverLetter,
        string skills, string education, string? notes)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        CurrentCompany = currentCompany;
        CurrentDesignation = currentDesignation;
        TotalExperience = totalExperience;
        ExpectedSalary = expectedSalary;
        Currency = currency;
        ResumeUrl = resumeUrl;
        CoverLetter = coverLetter;
        Skills = skills;
        Education = education;
        Notes = notes;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateStatus(CandidateStatus status, string? rejectionReason = null)
    {
        Status = status;
        if (rejectionReason != null)
            RejectionReason = rejectionReason;
        UpdatedAt = DateTime.UtcNow;
    }
}
