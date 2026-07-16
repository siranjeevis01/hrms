using HRMS.Services.Performance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Performance.Infrastructure.Persistence.Configurations;

public class CalibrationSessionConfiguration : IEntityTypeConfiguration<CalibrationSession>
{
    public void Configure(EntityTypeBuilder<CalibrationSession> builder)
    {
        builder.ToTable("CalibrationSessions");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Description)
            .HasMaxLength(1000);

        builder.Property(e => e.ReviewPeriod)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(e => e.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(e => e.ConductedBy);
        builder.HasIndex(e => e.Status);
        builder.HasIndex(e => e.TenantId);

        builder.HasMany(e => e.Entries)
            .WithOne()
            .HasForeignKey(e => e.CalibrationSessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(e => e.DomainEvents);
    }
}
