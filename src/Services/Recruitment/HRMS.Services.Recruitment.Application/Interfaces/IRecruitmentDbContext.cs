using HRMS.Services.Recruitment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Recruitment.Application.Interfaces;

public interface IRecruitmentDbContext
{
    DbSet<Candidate> Candidates { get; }
    DbSet<JobPosting> JobPostings { get; }
    DbSet<JobApplication> JobApplications { get; }
    DbSet<Interview> Interviews { get; }
    DbSet<InterviewFeedback> InterviewFeedbacks { get; }
    DbSet<OfferLetter> OfferLetters { get; }
    DbSet<OnboardingChecklist> OnboardingChecklists { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
