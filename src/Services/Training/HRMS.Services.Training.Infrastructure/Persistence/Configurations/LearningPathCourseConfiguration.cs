using HRMS.Services.Training.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Training.Infrastructure.Persistence.Configurations;

public class LearningPathCourseConfiguration : IEntityTypeConfiguration<LearningPathCourse>
{
    public void Configure(EntityTypeBuilder<LearningPathCourse> builder)
    {
        builder.ToTable("LearningPathCourses");

        builder.HasKey(lpc => lpc.Id);

        builder.HasOne<LearningPath>()
            .WithMany(lp => lp.LearningPathCourses)
            .HasForeignKey(lpc => lpc.LearningPathId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(lpc => !lpc.IsDeleted);
    }
}
