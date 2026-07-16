using HRMS.Services.Report.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Report.Infrastructure.Persistence.Configurations;

public class ScheduledReportConfiguration : IEntityTypeConfiguration<ScheduledReport>
{
    public void Configure(EntityTypeBuilder<ScheduledReport> builder)
    {
        builder.ToTable("ScheduledReports");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.CronExpression)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Recipients)
            .HasMaxLength(4000);

        builder.Property(s => s.Parameters)
            .HasMaxLength(4000);

        builder.Property(s => s.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(s => s.TemplateId);
        builder.HasIndex(s => s.IsActive);
        builder.HasIndex(s => s.TenantId);

        builder.Ignore(s => s.DomainEvents);
    }
}
