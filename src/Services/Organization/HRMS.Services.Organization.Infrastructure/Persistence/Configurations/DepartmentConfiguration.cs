using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HRMS.Services.Organization.Domain.Entities;

namespace HRMS.Services.Organization.Infrastructure.Persistence.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("Departments");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(d => d.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(d => d.Description)
            .HasMaxLength(1000);

        builder.Property(d => d.CompanyId)
            .IsRequired();

        builder.Property(d => d.BranchId);

        builder.Property(d => d.ParentDepartmentId);

        builder.HasIndex(d => d.CompanyId);
        builder.HasIndex(d => d.BranchId);
        builder.HasIndex(d => d.ParentDepartmentId);
        builder.HasIndex(d => d.Code);
        builder.HasIndex(d => d.TenantId);
    }
}
