using HRMS.Services.Leave.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Leave.Infrastructure.Persistence.Configurations;

public class LeaveAccrualPolicyConfiguration : IEntityTypeConfiguration<LeaveAccrualPolicy>
{
    public void Configure(EntityTypeBuilder<LeaveAccrualPolicy> builder)
    {
        builder.ToTable("LeaveAccrualPolicies");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.LeaveTypeId)
            .IsRequired();

        builder.Property(e => e.CompanyId)
            .IsRequired();

        builder.Property(e => e.AccrualFrequency)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(e => e.AccrualDay)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(e => e.MaxAccrualPerYear)
            .HasPrecision(7, 2);

        builder.Property(e => e.ResetType)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(e => e.TenantId)
            .IsRequired();

        builder.HasIndex(e => new { e.LeaveTypeId, e.CompanyId })
            .IsUnique();

        builder.HasIndex(e => e.TenantId);
    }
}
