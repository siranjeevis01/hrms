using HRMS.Services.ProjectTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.ProjectTask.Infrastructure.Persistence.Configurations;

public class StoryConfiguration : IEntityTypeConfiguration<Story>
{
    public void Configure(EntityTypeBuilder<Story> builder)
    {
        builder.ToTable("Stories");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(e => e.Description)
            .HasMaxLength(4000);

        builder.Property(e => e.AcceptanceCriteria)
            .HasMaxLength(4000);

        builder.HasIndex(e => e.EpicId);
        builder.HasIndex(e => e.ProjectId);
        builder.HasIndex(e => e.AssignedTo);
        builder.HasIndex(e => e.SprintId);
    }
}
