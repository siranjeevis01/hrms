using HRMS.Shared.Kernel.Common;
using HRMS.Services.Recruitment.Domain.Enums;

namespace HRMS.Services.Recruitment.Domain.Entities;

public class JobApplication : BaseEntity
{
    public Guid JobPostingId { get; private set; }
    public Guid CandidateId { get; private set; }
    public DateTime AppliedAt { get; private set; }
    public ApplicationStatus Status { get; private set; }
    public decimal? ScreeningScore { get; private set; }
    public string? RecruiterNotes { get; private set; }
    public Guid? AssignedTo { get; private set; }
    public string? RejectionReason { get; private set; }
    public Guid TenantId { get; private set; }

    public JobPosting? JobPosting { get; set; }
    public Candidate? Candidate { get; set; }

    private JobApplication() { }

    public static JobApplication Create(Guid jobPostingId, Guid candidateId, Guid tenantId)
    {
        return new JobApplication
        {
            Id = Guid.NewGuid(),
            JobPostingId = jobPostingId,
            CandidateId = candidateId,
            AppliedAt = DateTime.UtcNow,
            Status = ApplicationStatus.Applied,
            TenantId = tenantId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void UpdateStatus(ApplicationStatus status, string? rejectionReason = null)
    {
        Status = status;
        if (rejectionReason != null)
            RejectionReason = rejectionReason;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetScreeningScore(decimal score, string? notes)
    {
        ScreeningScore = score;
        RecruiterNotes = notes;
        Status = ApplicationStatus.Screening;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Shortlist(Guid assignedTo)
    {
        Status = ApplicationStatus.Shortlisted;
        AssignedTo = assignedTo;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AssignTo(Guid userId)
    {
        AssignedTo = userId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reject(string reason)
    {
        Status = ApplicationStatus.Rejected;
        RejectionReason = reason;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Withdraw()
    {
        Status = ApplicationStatus.Withdrawn;
        UpdatedAt = DateTime.UtcNow;
    }
}
