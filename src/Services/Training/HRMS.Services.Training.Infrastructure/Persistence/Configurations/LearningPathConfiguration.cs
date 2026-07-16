using HRMS.Services.Training.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Training.Infrastructure.Persistence.Configurations;

public class LearningPathConfiguration : IEntityTypeConfiguration<LearningPath>
{
    public void Configure(EntityTypeBuilder<LearningPath> builder)
    {
        builder.ToTable("LearningPaths");

        builder.HasKey(lp => lp.Id);

        builder.Property(lp => lp.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(lp => lp.Description)
            .HasMaxLength(2000);

        builder.Property(lp => lp.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasQueryFilter(lp => !lp.IsDeleted);
    }
}
