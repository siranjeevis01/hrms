using HRMS.Services.Helpdesk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Helpdesk.Infrastructure.Persistence.Configurations;

public class HelpdeskTicketConfiguration : IEntityTypeConfiguration<HelpdeskTicket>
{
    public void Configure(EntityTypeBuilder<HelpdeskTicket> builder)
    {
        builder.ToTable("HelpdeskTickets");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Subject)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.Description)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(t => t.ResolutionNotes)
            .HasMaxLength(2000);

        builder.Property(t => t.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(t => t.EmployeeId);
        builder.HasIndex(t => t.Status);
        builder.HasIndex(t => t.Priority);
        builder.HasIndex(t => t.Category);
        builder.HasIndex(t => t.AssignedTo);
        builder.HasIndex(t => t.TenantId);

        builder.HasMany(t => t.Comments)
            .WithOne()
            .HasForeignKey(c => c.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.Attachments)
            .WithOne()
            .HasForeignKey(a => a.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(t => t.DomainEvents);
    }
}
