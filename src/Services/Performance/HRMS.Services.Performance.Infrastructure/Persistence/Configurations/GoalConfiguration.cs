using HRMS.Services.Performance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Performance.Infrastructure.Persistence.Configurations;

public class GoalConfiguration : IEntityTypeConfiguration<Goal>
{
    public void Configure(EntityTypeBuilder<Goal> builder)
    {
        builder.ToTable("Goals");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Description)
            .HasMaxLength(2000);

        builder.Property(e => e.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Unit)
            .HasMaxLength(50);

        builder.HasIndex(e => e.EmployeeId);
        builder.HasIndex(e => e.ManagerId);
        builder.HasIndex(e => e.DepartmentId);
        builder.HasIndex(e => e.Status);
        builder.HasIndex(e => e.Category);
        builder.HasIndex(e => e.TenantId);

        builder.HasMany(e => e.KeyResults)
            .WithOne()
            .HasForeignKey(kr => kr.GoalId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(e => e.DomainEvents);
    }
}
