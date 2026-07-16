using HRMS.Services.Training.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Training.Infrastructure.Persistence.Configurations;

public class AssessmentAttemptConfiguration : IEntityTypeConfiguration<AssessmentAttempt>
{
    public void Configure(EntityTypeBuilder<AssessmentAttempt> builder)
    {
        builder.ToTable("AssessmentAttempts");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Answers)
            .HasColumnType("nvarchar(max)");

        builder.HasOne<Assessment>()
            .WithMany()
            .HasForeignKey(a => a.AssessmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(a => !a.IsDeleted);
    }
}
