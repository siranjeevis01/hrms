using HRMS.Services.Employee.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Employee.Infrastructure.Persistence.Configurations;

public class EmployeeHistoryConfiguration : IEntityTypeConfiguration<EmployeeHistory>
{
    public void Configure(EntityTypeBuilder<EmployeeHistory> builder)
    {
        builder.ToTable("EmployeeHistories");

        builder.HasKey(h => h.Id);

        builder.Property(h => h.PreviousValue)
            .HasMaxLength(500);

        builder.Property(h => h.NewValue)
            .HasMaxLength(500);

        builder.Property(h => h.Reason)
            .HasMaxLength(1000);

        builder.Property(h => h.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(h => h.EmployeeId);
        builder.HasIndex(h => h.ActionDate);
    }
}
