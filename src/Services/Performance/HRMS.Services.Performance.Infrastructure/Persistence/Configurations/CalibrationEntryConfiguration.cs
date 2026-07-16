using HRMS.Services.Performance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Performance.Infrastructure.Persistence.Configurations;

public class CalibrationEntryConfiguration : IEntityTypeConfiguration<CalibrationEntry>
{
    public void Configure(EntityTypeBuilder<CalibrationEntry> builder)
    {
        builder.ToTable("CalibrationEntries");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Justification)
            .HasMaxLength(1000);

        builder.Property(e => e.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(e => e.CalibrationSessionId);
        builder.HasIndex(e => e.EmployeeId);
    }
}
