using HRMS.Services.Recruitment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Recruitment.Infrastructure.Persistence.Configurations;

public class JobApplicationConfiguration : IEntityTypeConfiguration<JobApplication>
{
    public void Configure(EntityTypeBuilder<JobApplication> builder)
    {
        builder.ToTable("JobApplications");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.RecruiterNotes)
            .HasMaxLength(2000);

        builder.Property(a => a.RejectionReason)
            .HasMaxLength(500);

        builder.HasIndex(a => new { a.JobPostingId, a.CandidateId }).IsUnique();
        builder.HasIndex(a => a.Status);
        builder.HasIndex(a => a.TenantId);

        builder.HasOne(a => a.JobPosting)
            .WithMany(j => j.Applications)
            .HasForeignKey(a => a.JobPostingId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Candidate)
            .WithMany()
            .HasForeignKey(a => a.CandidateId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
