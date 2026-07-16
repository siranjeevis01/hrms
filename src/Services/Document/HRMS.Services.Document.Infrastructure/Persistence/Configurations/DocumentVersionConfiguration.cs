using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DocEntities = HRMS.Services.Document.Domain.Entities;

namespace HRMS.Services.Document.Infrastructure.Persistence.Configurations;

public class DocumentVersionConfiguration : IEntityTypeConfiguration<DocEntities.DocumentVersion>
{
    public void Configure(EntityTypeBuilder<DocEntities.DocumentVersion> builder)
    {
        builder.ToTable("DocumentVersions");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.FileUrl)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(v => v.ChangeNotes)
            .HasMaxLength(1000);

        builder.Ignore(v => v.TenantId);

        builder.HasIndex(v => v.DocumentId);
    }
}
