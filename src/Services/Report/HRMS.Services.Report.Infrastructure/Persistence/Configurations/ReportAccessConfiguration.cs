using HRMS.Services.Report.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Report.Infrastructure.Persistence.Configurations;

public class ReportAccessConfiguration : IEntityTypeConfiguration<ReportAccess>
{
    public void Configure(EntityTypeBuilder<ReportAccess> builder)
    {
        builder.ToTable("ReportAccesses");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.TemplateId)
            .IsRequired();

        builder.Property(a => a.Permission)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(a => a.TemplateId);
        builder.HasIndex(a => a.UserId);
        builder.HasIndex(a => a.TenantId);
    }
}
