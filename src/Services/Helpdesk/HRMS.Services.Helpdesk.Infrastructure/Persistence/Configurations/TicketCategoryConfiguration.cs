using HRMS.Services.Helpdesk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Helpdesk.Infrastructure.Persistence.Configurations;

public class TicketCategoryConfiguration : IEntityTypeConfiguration<TicketCategoryEntity>
{
    public void Configure(EntityTypeBuilder<TicketCategoryEntity> builder)
    {
        builder.ToTable("TicketCategories");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Code)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(c => c.Description)
            .HasMaxLength(500);

        builder.Property(c => c.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(c => c.Code);
        builder.HasIndex(c => c.IsActive);
        builder.HasIndex(c => c.TenantId);
    }
}
