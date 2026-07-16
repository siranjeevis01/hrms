using HRMS.Services.Leave.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Leave.Infrastructure.Persistence.Configurations;

public class LeavePolicyConfiguration : IEntityTypeConfiguration<LeavePolicy>
{
    public void Configure(EntityTypeBuilder<LeavePolicy> builder)
    {
        builder.ToTable("LeavePolicies");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.CompanyId)
            .IsRequired();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Description)
            .HasMaxLength(1000);

        builder.Property(e => e.TenantId)
            .IsRequired();

        builder.HasIndex(e => new { e.CompanyId, e.TenantId })
            .IsUnique();

        builder.HasIndex(e => e.TenantId);
    }
}
