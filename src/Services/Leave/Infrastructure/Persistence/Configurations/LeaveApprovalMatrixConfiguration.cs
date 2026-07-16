using HRMS.Services.Leave.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Leave.Infrastructure.Persistence.Configurations;

public class LeaveApprovalMatrixConfiguration : IEntityTypeConfiguration<LeaveApprovalMatrix>
{
    public void Configure(EntityTypeBuilder<LeaveApprovalMatrix> builder)
    {
        builder.ToTable("LeaveApprovalMatrices");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.LeaveTypeId)
            .IsRequired();

        builder.Property(e => e.CompanyId)
            .IsRequired();

        builder.Property(e => e.Level)
            .IsRequired();

        builder.Property(e => e.ApproverType)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(e => e.TenantId)
            .IsRequired();

        builder.HasIndex(e => new { e.LeaveTypeId, e.CompanyId, e.Level })
            .IsUnique();

        builder.HasIndex(e => e.TenantId);
    }
}
