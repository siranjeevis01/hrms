using HRMS.Services.Travel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Travel.Infrastructure.Persistence.Configurations;

public class TravelRequestConfiguration : IEntityTypeConfiguration<TravelRequest>
{
    public void Configure(EntityTypeBuilder<TravelRequest> builder)
    {
        builder.ToTable("TravelRequests");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Purpose)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(t => t.Destination)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.EstimatedCost)
            .HasPrecision(18, 2);

        builder.Property(t => t.ActualCost)
            .HasPrecision(18, 2);

        builder.Property(t => t.Currency)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(t => t.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(t => t.EmployeeId);
        builder.HasIndex(t => t.Status);
        builder.HasIndex(t => t.TenantId);

        builder.HasMany(t => t.Itineraries)
            .WithOne()
            .HasForeignKey(i => i.TravelRequestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.Expenses)
            .WithOne()
            .HasForeignKey(e => e.TravelRequestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.Approvals)
            .WithOne()
            .HasForeignKey(a => a.TravelRequestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(t => t.DomainEvents);
    }
}
