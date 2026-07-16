using HRMS.Services.Leave.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Leave.Infrastructure.Persistence.Configurations;

public class LeaveCommentConfiguration : IEntityTypeConfiguration<LeaveComment>
{
    public void Configure(EntityTypeBuilder<LeaveComment> builder)
    {
        builder.ToTable("LeaveComments");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.LeaveApplicationId)
            .IsRequired();

        builder.Property(e => e.UserId)
            .IsRequired();

        builder.Property(e => e.Comment)
            .IsRequired()
            .HasMaxLength(2000);

        builder.HasIndex(e => e.LeaveApplicationId);
    }
}
