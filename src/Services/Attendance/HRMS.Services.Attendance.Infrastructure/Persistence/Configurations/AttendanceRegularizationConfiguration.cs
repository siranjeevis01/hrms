using HRMS.Services.Attendance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Attendance.Infrastructure.Persistence.Configurations;

public class AttendanceRegularizationConfiguration : IEntityTypeConfiguration<AttendanceRegularization>
{
    public void Configure(EntityTypeBuilder<AttendanceRegularization> builder)
    {
        builder.ToTable("AttendanceRegularizations");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.AttendanceRecordId)
            .IsRequired();

        builder.Property(r => r.EmployeeId)
            .IsRequired();

        builder.Property(r => r.Reason)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(r => r.RequestedDate)
            .IsRequired();

        builder.Property(r => r.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(r => r.RejectionReason)
            .HasMaxLength(500);

        builder.Property(r => r.TenantId)
            .IsRequired();

        builder.HasIndex(r => r.AttendanceRecordId);
        builder.HasIndex(r => r.EmployeeId);
        builder.HasIndex(r => r.Status);
        builder.HasIndex(r => r.TenantId);
    }
}
