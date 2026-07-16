using HRMS.Services.Employee.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Services.Employee.Infrastructure.Persistence.Configurations;

public class EmployeeDocumentConfiguration : IEntityTypeConfiguration<EmployeeDocument>
{
    public void Configure(EntityTypeBuilder<EmployeeDocument> builder)
    {
        builder.ToTable("EmployeeDocuments");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.FileName)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(d => d.FileUrl)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(d => d.MimeType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.TenantId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(d => d.EmployeeId);
        builder.HasIndex(d => d.DocumentType);
    }
}
