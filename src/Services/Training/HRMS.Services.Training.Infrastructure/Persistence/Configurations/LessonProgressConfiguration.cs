using HRMS.Services.Training.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Training.Infrastructure.Persistence.Configurations;

public class LessonProgressConfiguration : IEntityTypeConfiguration<LessonProgress>
{
    public void Configure(EntityTypeBuilder<LessonProgress> builder)
    {
        builder.ToTable("LessonProgresses");

        builder.HasKey(lp => lp.Id);

        builder.Property(lp => lp.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasOne<Enrollment>()
            .WithMany(e => e.LessonProgresses)
            .HasForeignKey(lp => lp.EnrollmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(lp => !lp.IsDeleted);
    }
}
