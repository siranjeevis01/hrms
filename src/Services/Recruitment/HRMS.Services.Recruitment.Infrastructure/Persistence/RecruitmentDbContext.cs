using HRMS.Services.Recruitment.Application.Interfaces;
using HRMS.Services.Recruitment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Recruitment.Infrastructure.Persistence;

public class RecruitmentDbContext : DbContext, IRecruitmentDbContext
{
    public RecruitmentDbContext(DbContextOptions<RecruitmentDbContext> options) : base(options) { }

    public DbSet<Candidate> Candidates => Set<Candidate>();
    public DbSet<JobPosting> JobPostings => Set<JobPosting>();
    public DbSet<JobApplication> JobApplications => Set<JobApplication>();
    public DbSet<Interview> Interviews => Set<Interview>();
    public DbSet<InterviewFeedback> InterviewFeedbacks => Set<InterviewFeedback>();
    public DbSet<OfferLetter> OfferLetters => Set<OfferLetter>();
    public DbSet<OnboardingChecklist> OnboardingChecklists => Set<OnboardingChecklist>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RecruitmentDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
