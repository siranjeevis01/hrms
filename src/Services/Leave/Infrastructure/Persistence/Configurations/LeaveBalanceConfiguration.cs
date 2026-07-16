using HRMS.Services.Leave.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Leave.Infrastructure.Persistence.Configurations;

public class LeaveBalanceConfiguration : IEntityTypeConfiguration<LeaveBalance>
{
    public void Configure(EntityTypeBuilder<LeaveBalance> builder)
    {
        builder.ToTable("LeaveBalances");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.EmployeeId)
            .IsRequired();

        builder.Property(e => e.LeaveTypeId)
            .IsRequired();

        builder.Property(e => e.Year)
            .IsRequired();

        builder.Property(e => e.TotalDays)
            .HasPrecision(7, 2);

        builder.Property(e => e.UsedDays)
            .HasPrecision(7, 2);

        builder.Property(e => e.PendingDays)
            .HasPrecision(7, 2);

        builder.Property(e => e.CarryForwardDays)
            .HasPrecision(7, 2);

        builder.Property(e => e.EncashedDays)
            .HasPrecision(7, 2);

        builder.Property(e => e.AdjustedDays)
            .HasPrecision(7, 2);

        builder.Property(e => e.TenantId)
            .IsRequired();

        builder.HasIndex(e => new { e.EmployeeId, e.LeaveTypeId, e.Year })
            .IsUnique();

        builder.HasIndex(e => e.TenantId);
    }
}
