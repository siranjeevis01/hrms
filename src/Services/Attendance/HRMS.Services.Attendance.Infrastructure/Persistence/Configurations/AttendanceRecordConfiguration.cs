using HRMS.Services.Attendance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Attendance.Infrastructure.Persistence.Configurations;

public class AttendanceRecordConfiguration : IEntityTypeConfiguration<AttendanceRecord>
{
    public void Configure(EntityTypeBuilder<AttendanceRecord> builder)
    {
        builder.ToTable("AttendanceRecords");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.EmployeeId)
            .IsRequired();

        builder.Property(a => a.Date)
            .IsRequired();

        builder.Property(a => a.CheckInTime);

        builder.Property(a => a.CheckOutTime);

        builder.Property(a => a.ShiftId);

        builder.Property(a => a.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(a => a.CheckInMethod)
            .HasConversion<int?>();

        builder.Property(a => a.CheckOutMethod)
            .HasConversion<int?>();

        builder.Property(a => a.CheckInLatitude);

        builder.Property(a => a.CheckInLongitude);

        builder.Property(a => a.CheckOutLatitude);

        builder.Property(a => a.CheckOutLongitude);

        builder.Property(a => a.WifiSSID)
            .HasMaxLength(100);

        builder.Property(a => a.WifiBSSID)
            .HasMaxLength(50);

        builder.Property(a => a.QrCodeId)
            .HasMaxLength(100);

        builder.Property(a => a.TotalHours)
            .HasPrecision(5, 2);

        builder.Property(a => a.OvertimeHours)
            .HasPrecision(5, 2);

        builder.Property(a => a.BreakMinutes)
            .HasDefaultValue(0);

        builder.Property(a => a.Notes)
            .HasMaxLength(500);

        builder.Property(a => a.TenantId)
            .IsRequired();

        builder.HasIndex(a => new { a.EmployeeId, a.Date })
            .IsUnique();

        builder.HasIndex(a => a.EmployeeId);
        builder.HasIndex(a => a.Date);
        builder.HasIndex(a => a.Status);
        builder.HasIndex(a => a.TenantId);
        builder.HasIndex(a => a.IsLate);

        builder.Ignore(a => a.DomainEvents);
    }
}
