using HRMS.Services.ProjectTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.ProjectTask.Infrastructure.Persistence.Configurations;

public class TimeLogConfiguration : IEntityTypeConfiguration<TimeLog>
{
    public void Configure(EntityTypeBuilder<TimeLog> builder)
    {
        builder.ToTable("TimeLogs");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Hours)
            .HasColumnType("decimal(8,2)");

        builder.Property(e => e.Description)
            .HasMaxLength(1000);

        builder.HasIndex(e => e.TaskItemId);
        builder.HasIndex(e => e.StoryId);
        builder.HasIndex(e => e.EmployeeId);
        builder.HasIndex(e => e.Date);
    }
}
