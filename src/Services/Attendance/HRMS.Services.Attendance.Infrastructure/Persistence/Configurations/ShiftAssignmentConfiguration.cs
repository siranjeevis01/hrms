using HRMS.Services.Attendance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Attendance.Infrastructure.Persistence.Configurations;

public class ShiftAssignmentConfiguration : IEntityTypeConfiguration<ShiftAssignment>
{
    public void Configure(EntityTypeBuilder<ShiftAssignment> builder)
    {
        builder.ToTable("ShiftAssignments");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.EmployeeId)
            .IsRequired();

        builder.Property(s => s.ShiftId)
            .IsRequired();

        builder.Property(s => s.EffectiveFrom)
            .IsRequired();

        builder.Property(s => s.EffectiveTo);

        builder.Property(s => s.IsCurrent)
            .IsRequired();

        builder.Property(s => s.TenantId)
            .IsRequired();

        builder.HasIndex(s => s.EmployeeId);
        builder.HasIndex(s => s.IsCurrent);
        builder.HasIndex(s => s.TenantId);
        builder.HasIndex(s => new { s.EmployeeId, s.IsCurrent });
    }
}
