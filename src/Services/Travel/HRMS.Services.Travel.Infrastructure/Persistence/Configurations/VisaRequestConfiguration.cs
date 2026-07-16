using HRMS.Services.Travel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Travel.Infrastructure.Persistence.Configurations;

public class VisaRequestConfiguration : IEntityTypeConfiguration<VisaRequest>
{
    public void Configure(EntityTypeBuilder<VisaRequest> builder)
    {
        builder.ToTable("VisaRequests");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Country)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(v => v.VisaType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(v => v.Purpose)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(v => v.DocumentUrl)
            .HasMaxLength(500);

        builder.Property(v => v.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(v => v.EmployeeId);
        builder.HasIndex(v => v.Status);
        builder.HasIndex(v => v.TenantId);

        builder.Ignore(v => v.DomainEvents);
    }
}
