using HRMS.Services.Helpdesk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Helpdesk.Infrastructure.Persistence.Configurations;

public class FaqConfiguration : IEntityTypeConfiguration<Faq>
{
    public void Configure(EntityTypeBuilder<Faq> builder)
    {
        builder.ToTable("Faqs");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Question)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(f => f.Answer)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(f => f.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(f => f.CategoryId);
        builder.HasIndex(f => f.IsActive);
        builder.HasIndex(f => f.TenantId);
    }
}
