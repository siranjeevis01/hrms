using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.ProjectTask.Infrastructure.Persistence.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Domain.Entities.Project>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Project> builder)
    {
        builder.ToTable("Projects");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Description)
            .HasMaxLength(2000);

        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(e => e.ClientName)
            .HasMaxLength(200);

        builder.Property(e => e.Currency)
            .HasMaxLength(10);

        builder.Property(e => e.TenantId)
            .IsRequired();

        builder.Property(e => e.Budget)
            .HasColumnType("decimal(18,2)");

        builder.Property(e => e.ActualCost)
            .HasColumnType("decimal(18,2)");

        builder.Property(e => e.ProgressPercentage)
            .HasColumnType("decimal(5,2)");

        builder.HasIndex(e => e.Code).IsUnique();
        builder.HasIndex(e => e.DepartmentId);
        builder.HasIndex(e => e.Status);
        builder.HasIndex(e => e.TenantId);

        builder.HasMany(e => e.Members)
            .WithOne()
            .HasForeignKey(m => m.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Epics)
            .WithOne()
            .HasForeignKey(e => e.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Sprints)
            .WithOne()
            .HasForeignKey(s => s.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Boards)
            .WithOne()
            .HasForeignKey(b => b.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(e => e.DomainEvents);
    }
}
