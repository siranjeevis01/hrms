using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DocEntities = HRMS.Services.Document.Domain.Entities;

namespace HRMS.Services.Document.Infrastructure.Persistence.Configurations;

public class DocumentTemplateConfiguration : IEntityTypeConfiguration<DocEntities.DocumentTemplate>
{
    public void Configure(EntityTypeBuilder<DocEntities.DocumentTemplate> builder)
    {
        builder.ToTable("DocumentTemplates");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.Description)
            .HasMaxLength(1000);

        builder.Property(t => t.FileUrl)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(t => t.Placeholders)
            .HasMaxLength(2000);

        builder.Ignore(t => t.TenantId);

        builder.HasIndex(t => t.Category);

        builder.Ignore(t => t.DomainEvents);
    }
}
