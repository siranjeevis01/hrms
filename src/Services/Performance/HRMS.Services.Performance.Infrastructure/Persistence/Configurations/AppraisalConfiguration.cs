using HRMS.Services.Performance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Performance.Infrastructure.Persistence.Configurations;

public class AppraisalConfiguration : IEntityTypeConfiguration<Appraisal>
{
    public void Configure(EntityTypeBuilder<Appraisal> builder)
    {
        builder.ToTable("Appraisals");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Period)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(e => e.Comments)
            .HasMaxLength(2000);

        builder.Property(e => e.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(e => e.EmployeeId);
        builder.HasIndex(e => e.ManagerId);
        builder.HasIndex(e => e.Status);
        builder.HasIndex(e => e.Type);
        builder.HasIndex(e => e.Period);
        builder.HasIndex(e => e.TenantId);

        builder.Ignore(e => e.DomainEvents);
    }
}
