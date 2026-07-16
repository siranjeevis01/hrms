using HRMS.Shared.Kernel.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DocEntities = HRMS.Services.Document.Domain.Entities;

namespace HRMS.Services.Document.Infrastructure.Persistence.Configurations;

public class DocumentConfiguration : IEntityTypeConfiguration<DocEntities.Document>
{
    public void Configure(EntityTypeBuilder<DocEntities.Document> builder)
    {
        builder.ToTable("Documents");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(d => d.FileName)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(d => d.ContentType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.FileUrl)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(d => d.ThumbnailUrl)
            .HasMaxLength(1000);

        builder.Property(d => d.Description)
            .HasMaxLength(2000);

        builder.Property(d => d.Tags)
            .HasMaxLength(2000);

        builder.Ignore(d => d.TenantId);

        builder.HasIndex(d => d.FolderId);
        builder.HasIndex(d => d.UploadedBy);
        builder.HasIndex(d => d.Status);
        builder.HasIndex(d => d.Category);

        builder.HasMany(d => d.Versions)
            .WithOne()
            .HasForeignKey(v => v.DocumentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.Shares)
            .WithOne()
            .HasForeignKey(s => s.DocumentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.AccessLogs)
            .WithOne()
            .HasForeignKey(l => l.DocumentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(d => d.DomainEvents);
    }
}
