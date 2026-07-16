using HRMS.Services.Leave.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Leave.Infrastructure.Persistence.Configurations;

public class HolidayCalendarEntryConfiguration : IEntityTypeConfiguration<HolidayCalendarEntry>
{
    public void Configure(EntityTypeBuilder<HolidayCalendarEntry> builder)
    {
        builder.ToTable("HolidayCalendarEntries");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.HolidayId)
            .IsRequired();

        builder.Property(e => e.CompanyId)
            .IsRequired();

        builder.Property(e => e.Date)
            .IsRequired();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.TenantId)
            .IsRequired();

        builder.HasIndex(e => new { e.CompanyId, e.Date });
        builder.HasIndex(e => e.TenantId);
    }
}
