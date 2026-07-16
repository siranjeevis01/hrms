using HRMS.Services.Report.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Report.Infrastructure.Persistence.Configurations;

public class ReportTemplateConfiguration : IEntityTypeConfiguration<ReportTemplate>
{
    public void Configure(EntityTypeBuilder<ReportTemplate> builder)
    {
        builder.ToTable("ReportTemplates");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.Description)
            .HasMaxLength(1000);

        builder.Property(t => t.DataSource)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(t => t.QueryDefinition)
            .HasMaxLength(4000);

        builder.Property(t => t.Parameters)
            .HasMaxLength(4000);

        builder.Property(t => t.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(t => t.Category);
        builder.HasIndex(t => t.ReportType);
        builder.HasIndex(t => t.TenantId);

        builder.HasMany(t => t.Instances)
            .WithOne()
            .HasForeignKey(i => i.TemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.AccessPermissions)
            .WithOne()
            .HasForeignKey(a => a.TemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(t => t.DomainEvents);
    }
}
