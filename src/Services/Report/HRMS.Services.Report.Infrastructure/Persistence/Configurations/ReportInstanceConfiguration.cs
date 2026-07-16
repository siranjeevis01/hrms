using HRMS.Services.Report.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Report.Infrastructure.Persistence.Configurations;

public class ReportInstanceConfiguration : IEntityTypeConfiguration<ReportInstance>
{
    public void Configure(EntityTypeBuilder<ReportInstance> builder)
    {
        builder.ToTable("ReportInstances");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.TemplateId)
            .IsRequired();

        builder.Property(i => i.Parameters)
            .HasMaxLength(4000);

        builder.Property(i => i.FileUrl)
            .HasMaxLength(500);

        builder.Property(i => i.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(i => i.TemplateId);
        builder.HasIndex(i => i.Status);
        builder.HasIndex(i => i.GeneratedAt);
        builder.HasIndex(i => i.TenantId);
    }
}
