using HRMS.Services.Workflow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Workflow.Infrastructure.Persistence.Configurations;

public class NotificationRuleConfiguration : IEntityTypeConfiguration<NotificationRule>
{
    public void Configure(EntityTypeBuilder<NotificationRule> builder)
    {
        builder.ToTable("NotificationRules");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.WorkflowDefinitionId)
            .IsRequired();

        builder.Property(e => e.EventType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Channel)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.TemplateId)
            .HasMaxLength(100);

        builder.Property(e => e.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(e => e.WorkflowDefinitionId);
        builder.HasIndex(e => e.TenantId);

        builder.Ignore(e => e.DomainEvents);
    }
}
