using HRMS.Services.Recruitment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Recruitment.Infrastructure.Persistence.Configurations;

public class InterviewConfiguration : IEntityTypeConfiguration<Interview>
{
    public void Configure(EntityTypeBuilder<Interview> builder)
    {
        builder.ToTable("Interviews");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.InterviewerIds)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(i => i.Location)
            .HasMaxLength(200);

        builder.Property(i => i.MeetingUrl)
            .HasMaxLength(500);

        builder.HasIndex(i => i.ScheduledAt);
        builder.HasIndex(i => i.Status);
        builder.HasIndex(i => i.CandidateId);
        builder.HasIndex(i => i.TenantId);

        builder.HasOne(i => i.JobApplication)
            .WithMany()
            .HasForeignKey(i => i.JobApplicationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.Candidate)
            .WithMany()
            .HasForeignKey(i => i.CandidateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Ignore(i => i.DomainEvents);
    }
}
