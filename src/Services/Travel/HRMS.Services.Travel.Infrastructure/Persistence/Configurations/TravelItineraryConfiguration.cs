using HRMS.Services.Travel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Travel.Infrastructure.Persistence.Configurations;

public class TravelItineraryConfiguration : IEntityTypeConfiguration<TravelItinerary>
{
    public void Configure(EntityTypeBuilder<TravelItinerary> builder)
    {
        builder.ToTable("TravelItineraries");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Activity)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(i => i.Location)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(i => i.Notes)
            .HasMaxLength(500);

        builder.Property(i => i.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(i => i.TravelRequestId);
        builder.HasIndex(i => i.TenantId);
    }
}
