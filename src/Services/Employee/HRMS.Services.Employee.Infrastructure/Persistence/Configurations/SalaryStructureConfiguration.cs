using HRMS.Services.Employee.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Employee.Infrastructure.Persistence.Configurations;

public class SalaryStructureConfiguration : IEntityTypeConfiguration<SalaryStructure>
{
    public void Configure(EntityTypeBuilder<SalaryStructure> builder)
    {
        builder.ToTable("SalaryStructures");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.BasicSalary)
            .HasColumnType("decimal(18,2)");

        builder.Property(s => s.GrossSalary)
            .HasColumnType("decimal(18,2)");

        builder.Property(s => s.CTC)
            .HasColumnType("decimal(18,2)");

        builder.Property(s => s.Currency)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(s => s.ComponentDetails)
            .HasColumnType("nvarchar(max)");

        builder.HasIndex(s => s.EmployeeId);
        builder.HasIndex(s => new { s.EmployeeId, s.IsCurrent });
    }
}
