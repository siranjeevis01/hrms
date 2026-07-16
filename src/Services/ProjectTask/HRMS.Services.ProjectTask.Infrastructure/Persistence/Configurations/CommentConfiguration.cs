using HRMS.Services.ProjectTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.ProjectTask.Infrastructure.Persistence.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Content)
            .IsRequired()
            .HasMaxLength(4000);

        builder.HasIndex(e => e.TaskItemId);
        builder.HasIndex(e => e.StoryId);
        builder.HasIndex(e => e.BugId);
        builder.HasIndex(e => e.EmployeeId);
    }
}
