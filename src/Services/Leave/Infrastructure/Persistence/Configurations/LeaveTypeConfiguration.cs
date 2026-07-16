using HRMS.Services.Leave.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Leave.Infrastructure.Persistence.Configurations;

public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
{
    public void Configure(EntityTypeBuilder<LeaveType> builder)
    {
        builder.ToTable("LeaveTypes");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Description)
            .HasMaxLength(1000);

        builder.Property(e => e.Color)
            .HasMaxLength(20);

        builder.Property(e => e.Icon)
            .HasMaxLength(100);

        builder.Property(e => e.Gender)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(e => e.AccrualType)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(e => e.AccrualRate)
            .HasPrecision(5, 2);

        builder.Property(e => e.TenantId)
            .IsRequired();

        builder.HasIndex(e => e.Code)
            .IsUnique();

        builder.HasIndex(e => e.TenantId);
        builder.HasIndex(e => e.IsActive);
    }
}
