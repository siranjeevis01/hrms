using Microsoft.EntityFrameworkCore;
using DocEntities = HRMS.Services.Document.Domain.Entities;

namespace HRMS.Services.Document.Application.Interfaces;

public interface IDocumentDbContext
{
    DbSet<DocEntities.Document> Documents { get; }
    DbSet<DocEntities.DocumentFolder> DocumentFolders { get; }
    DbSet<DocEntities.DocumentVersion> DocumentVersions { get; }
    DbSet<DocEntities.DocumentShare> DocumentShares { get; }
    DbSet<DocEntities.DocumentAccessLog> DocumentAccessLogs { get; }
    DbSet<DocEntities.DocumentTemplate> DocumentTemplates { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
