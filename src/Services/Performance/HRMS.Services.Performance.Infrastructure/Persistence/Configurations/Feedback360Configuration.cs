using HRMS.Services.Performance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Performance.Infrastructure.Persistence.Configurations;

public class Feedback360Configuration : IEntityTypeConfiguration<Feedback360>
{
    public void Configure(EntityTypeBuilder<Feedback360> builder)
    {
        builder.ToTable("Feedback360s");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.ReviewPeriod)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(e => e.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(e => e.EmployeeId);
        builder.HasIndex(e => e.ReviewerId);
        builder.HasIndex(e => e.Status);
        builder.HasIndex(e => e.TenantId);

        builder.HasMany(e => e.Answers)
            .WithOne()
            .HasForeignKey(a => a.Feedback360Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(e => e.DomainEvents);
    }
}
