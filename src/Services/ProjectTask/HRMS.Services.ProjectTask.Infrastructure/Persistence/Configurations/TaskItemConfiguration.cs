using HRMS.Services.ProjectTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.ProjectTask.Infrastructure.Persistence.Configurations;

public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.ToTable("TaskItems");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(e => e.Description)
            .HasMaxLength(4000);

        builder.Property(e => e.EstimatedHours)
            .HasColumnType("decimal(8,2)");

        builder.Property(e => e.LoggedHours)
            .HasColumnType("decimal(8,2)");

        builder.HasIndex(e => e.StoryId);
        builder.HasIndex(e => e.ProjectId);
        builder.HasIndex(e => e.AssignedTo);
        builder.HasIndex(e => e.Status);
    }
}
