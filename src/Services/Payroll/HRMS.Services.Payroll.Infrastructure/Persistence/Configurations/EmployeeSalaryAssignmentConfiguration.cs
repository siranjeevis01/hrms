using HRMS.Services.Payroll.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Payroll.Infrastructure.Persistence.Configurations;

public class EmployeeSalaryAssignmentConfiguration : IEntityTypeConfiguration<EmployeeSalaryAssignment>
{
    public void Configure(EntityTypeBuilder<EmployeeSalaryAssignment> builder)
    {
        builder.ToTable("EmployeeSalaryAssignments");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.EmployeeId).IsRequired();
        builder.Property(a => a.ComponentDefId).IsRequired();
        builder.Property(a => a.Amount).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(a => a.Percentage).HasColumnType("decimal(5,2)").IsRequired(false);
        builder.Property(a => a.EffectiveFrom).IsRequired();
        builder.Property(a => a.EffectiveTo).IsRequired(false);
        builder.Property(a => a.IsCurrent).HasDefaultValue(true);
        builder.Property(a => a.TenantId).IsRequired();

        builder.HasOne(a => a.ComponentDef)
            .WithMany()
            .HasForeignKey(a => a.ComponentDefId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(a => new { a.EmployeeId, a.ComponentDefId, a.IsCurrent });
    }
}
