using HRMS.Services.Payroll.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Payroll.Infrastructure.Persistence.Configurations;

public class PayrollAuditLogConfiguration : IEntityTypeConfiguration<PayrollAuditLog>
{
    public void Configure(EntityTypeBuilder<PayrollAuditLog> builder)
    {
        builder.ToTable("PayrollAuditLogs");

        builder.HasKey(al => al.Id);

        builder.Property(al => al.PayrollRunId).IsRequired();
        builder.Property(al => al.Action).IsRequired().HasMaxLength(100);
        builder.Property(al => al.PerformedBy).IsRequired().HasMaxLength(100);
        builder.Property(al => al.PerformedAt).IsRequired();
        builder.Property(al => al.OldValue).HasMaxLength(500);
        builder.Property(al => al.NewValue).HasMaxLength(500);
        builder.Property(al => al.Details).HasMaxLength(2000);

        builder.HasIndex(al => al.PayrollRunId);
    }
}
