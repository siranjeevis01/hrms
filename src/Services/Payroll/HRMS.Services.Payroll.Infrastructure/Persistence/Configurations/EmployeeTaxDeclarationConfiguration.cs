using HRMS.Services.Payroll.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Payroll.Infrastructure.Persistence.Configurations;

public class EmployeeTaxDeclarationConfiguration : IEntityTypeConfiguration<EmployeeTaxDeclaration>
{
    public void Configure(EntityTypeBuilder<EmployeeTaxDeclaration> builder)
    {
        builder.ToTable("EmployeeTaxDeclarations");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.EmployeeId).IsRequired();
        builder.Property(d => d.FinancialYear).IsRequired().HasMaxLength(20);
        builder.Property(d => d.DeclaredAmount).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(d => d.InvestedAmount).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(d => d.ProofSubmitted).HasDefaultValue(false);
        builder.Property(d => d.Status).IsRequired().HasConversion<int>();
        builder.Property(d => d.TenantId).IsRequired();

        builder.HasIndex(d => new { d.EmployeeId, d.FinancialYear }).IsUnique();
    }
}
