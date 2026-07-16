using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HRMS.Services.Organization.Domain.Entities;

namespace HRMS.Services.Organization.Infrastructure.Persistence.Configurations;

public class HolidayConfiguration : IEntityTypeConfiguration<Holiday>
{
    public void Configure(EntityTypeBuilder<Holiday> builder)
    {
        builder.ToTable("Holidays");

        builder.HasKey(h => h.Id);

        builder.Property(h => h.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(h => h.ApplicableDepartmentIdsJson)
            .HasMaxLength(4000);

        builder.Property(h => h.CompanyId)
            .IsRequired();

        builder.Property(h => h.BranchId);

        builder.HasIndex(h => h.CompanyId);
        builder.HasIndex(h => h.BranchId);
        builder.HasIndex(h => h.Date);
        builder.HasIndex(h => h.TenantId);
    }
}
