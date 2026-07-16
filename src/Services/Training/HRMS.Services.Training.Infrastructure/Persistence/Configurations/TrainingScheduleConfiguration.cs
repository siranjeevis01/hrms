using HRMS.Services.Training.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Training.Infrastructure.Persistence.Configurations;

public class TrainingScheduleConfiguration : IEntityTypeConfiguration<TrainingSchedule>
{
    public void Configure(EntityTypeBuilder<TrainingSchedule> builder)
    {
        builder.ToTable("TrainingSchedules");

        builder.HasKey(ts => ts.Id);

        builder.Property(ts => ts.Location)
            .HasMaxLength(200);

        builder.Property(ts => ts.InstructorName)
            .HasMaxLength(200);

        builder.Property(ts => ts.MeetingUrl)
            .HasMaxLength(500);

        builder.Property(ts => ts.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasOne<Course>()
            .WithMany()
            .HasForeignKey(ts => ts.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(ts => !ts.IsDeleted);
    }
}
