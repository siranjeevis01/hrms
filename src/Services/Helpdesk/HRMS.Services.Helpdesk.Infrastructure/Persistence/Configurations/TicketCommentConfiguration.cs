using HRMS.Services.Helpdesk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Helpdesk.Infrastructure.Persistence.Configurations;

public class TicketCommentConfiguration : IEntityTypeConfiguration<TicketComment>
{
    public void Configure(EntityTypeBuilder<TicketComment> builder)
    {
        builder.ToTable("TicketComments");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Content)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(c => c.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(c => c.TicketId);
        builder.HasIndex(c => c.EmployeeId);
        builder.HasIndex(c => c.TenantId);
    }
}
