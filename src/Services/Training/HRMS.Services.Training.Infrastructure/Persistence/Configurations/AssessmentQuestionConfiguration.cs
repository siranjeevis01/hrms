using HRMS.Services.Training.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Training.Infrastructure.Persistence.Configurations;

public class AssessmentQuestionConfiguration : IEntityTypeConfiguration<AssessmentQuestion>
{
    public void Configure(EntityTypeBuilder<AssessmentQuestion> builder)
    {
        builder.ToTable("AssessmentQuestions");

        builder.HasKey(q => q.Id);

        builder.Property(q => q.QuestionText)
            .IsRequired()
            .HasColumnType("nvarchar(max)");

        builder.Property(q => q.QuestionType)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(q => q.Options)
            .HasColumnType("nvarchar(max)");

        builder.Property(q => q.CorrectAnswer)
            .HasColumnType("nvarchar(max)");

        builder.HasOne<Assessment>()
            .WithMany(a => a.Questions)
            .HasForeignKey(q => q.AssessmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(q => !q.IsDeleted);
    }
}
