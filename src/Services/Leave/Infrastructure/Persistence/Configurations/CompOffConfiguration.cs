using HRMS.Services.Leave.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Leave.Infrastructure.Persistence.Configurations;

public class CompOffConfiguration : IEntityTypeConfiguration<CompOff>
{
    public void Configure(EntityTypeBuilder<CompOff> builder)
    {
        builder.ToTable("CompOffs");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.EmployeeId)
            .IsRequired();

        builder.Property(e => e.Days)
            .HasPrecision(5, 2);

        builder.Property(e => e.Reason)
            .HasMaxLength(1000);

        builder.Property(e => e.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(e => e.TenantId)
            .IsRequired();

        builder.HasIndex(e => e.EmployeeId);
        builder.HasIndex(e => e.Status);
        builder.HasIndex(e => e.TenantId);
    }
}
