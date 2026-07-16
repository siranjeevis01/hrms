using HRMS.Services.Attendance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Attendance.Infrastructure.Persistence.Configurations;

public class AttendanceSummaryConfiguration : IEntityTypeConfiguration<AttendanceSummary>
{
    public void Configure(EntityTypeBuilder<AttendanceSummary> builder)
    {
        builder.ToTable("AttendanceSummaries");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.EmployeeId)
            .IsRequired();

        builder.Property(s => s.Year)
            .IsRequired();

        builder.Property(s => s.Month)
            .IsRequired();

        builder.Property(s => s.TenantId)
            .IsRequired();

        builder.HasIndex(s => new { s.EmployeeId, s.Year, s.Month })
            .IsUnique();

        builder.HasIndex(s => s.EmployeeId);
        builder.HasIndex(s => s.TenantId);
    }
}
