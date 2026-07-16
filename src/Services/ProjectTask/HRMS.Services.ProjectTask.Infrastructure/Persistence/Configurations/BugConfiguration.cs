using HRMS.Services.ProjectTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.ProjectTask.Infrastructure.Persistence.Configurations;

public class BugConfiguration : IEntityTypeConfiguration<Bug>
{
    public void Configure(EntityTypeBuilder<Bug> builder)
    {
        builder.ToTable("Bugs");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(e => e.Description)
            .HasMaxLength(4000);

        builder.Property(e => e.StepsToReproduce)
            .HasMaxLength(4000);

        builder.Property(e => e.ExpectedBehavior)
            .HasMaxLength(2000);

        builder.Property(e => e.ActualBehavior)
            .HasMaxLength(2000);

        builder.HasIndex(e => e.StoryId);
        builder.HasIndex(e => e.ProjectId);
        builder.HasIndex(e => e.AssignedTo);
        builder.HasIndex(e => e.Status);
    }
}
