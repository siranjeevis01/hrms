using HRMS.Services.Attendance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Attendance.Infrastructure.Persistence.Configurations;

public class AttendancePolicyConfiguration : IEntityTypeConfiguration<AttendancePolicy>
{
    public void Configure(EntityTypeBuilder<AttendancePolicy> builder)
    {
        builder.ToTable("AttendancePolicies");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.CompanyId)
            .IsRequired();

        builder.Property(p => p.GracePeriodMinutes)
            .IsRequired();

        builder.Property(p => p.MaxLateAllowed)
            .IsRequired();

        builder.Property(p => p.LateDeductionMinutes)
            .IsRequired();

        builder.Property(p => p.AutoCheckoutTime)
            .IsRequired();

        builder.Property(p => p.HalfDayMinimumHours)
            .HasPrecision(4, 2)
            .IsRequired();

        builder.Property(p => p.FullDayMinimumHours)
            .HasPrecision(4, 2)
            .IsRequired();

        builder.Property(p => p.OvertimeEnabled)
            .IsRequired();

        builder.Property(p => p.OvertimeThresholdMinutes)
            .IsRequired();

        builder.Property(p => p.MaxOvertimeMinutes)
            .IsRequired();

        builder.Property(p => p.TenantId)
            .IsRequired();

        builder.HasIndex(p => p.CompanyId)
            .IsUnique();

        builder.HasIndex(p => p.TenantId);
    }
}
