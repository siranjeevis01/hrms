using HRMS.Services.Recruitment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Recruitment.Infrastructure.Persistence.Configurations;

public class InterviewFeedbackConfiguration : IEntityTypeConfiguration<InterviewFeedback>
{
    public void Configure(EntityTypeBuilder<InterviewFeedback> builder)
    {
        builder.ToTable("InterviewFeedbacks");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Strengths)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(f => f.Weaknesses)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(f => f.Comments)
            .HasMaxLength(2000);

        builder.HasIndex(f => f.InterviewId);
        builder.HasIndex(f => f.InterviewerId);

        builder.HasOne(f => f.Interview)
            .WithMany(i => i.Feedback)
            .HasForeignKey(f => f.InterviewId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
