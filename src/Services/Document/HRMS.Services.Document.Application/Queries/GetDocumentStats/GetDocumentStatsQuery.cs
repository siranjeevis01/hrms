using HRMS.Services.Document.Application.DTOs;
using HRMS.Services.Document.Application.Interfaces;
using HRMS.Services.Document.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Document.Application.Queries.GetDocumentStats;

public class GetDocumentStatsQuery : IRequest<DocumentStatsDto>
{
    public string? TenantId { get; set; }
}

public class GetDocumentStatsQueryHandler : IRequestHandler<GetDocumentStatsQuery, DocumentStatsDto>
{
    private readonly IDocumentDbContext _context;

    public GetDocumentStatsQueryHandler(IDocumentDbContext context)
    {
        _context = context;
    }

    public async Task<DocumentStatsDto> Handle(GetDocumentStatsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Documents.Where(d => !d.IsDeleted);

        if (!string.IsNullOrEmpty(request.TenantId))
            query = query.Where(d => d.TenantId == request.TenantId);

        var documents = await query.ToListAsync(cancellationToken);

        var stats = new DocumentStatsDto
        {
            TotalDocuments = documents.Count,
            ActiveDocuments = documents.Count(d => d.Status == DocumentStatus.Active),
            ArchivedDocuments = documents.Count(d => d.Status == DocumentStatus.Archived),
            DeletedDocuments = documents.Count(d => d.Status == DocumentStatus.Deleted),
            TotalFileSize = documents.Sum(d => d.FileSize),
            TotalFolders = await _context.DocumentFolders.CountAsync(f => !f.IsDeleted, cancellationToken),
            TotalTemplates = await _context.DocumentTemplates.CountAsync(t => !t.IsDeleted, cancellationToken),
            TotalShares = await _context.DocumentShares.CountAsync(s => !s.IsDeleted, cancellationToken),
            TotalAccessLogs = await _context.DocumentAccessLogs.CountAsync(cancellationToken),
            DocumentsByCategory = documents
                .GroupBy(d => d.Category.ToString())
                .ToDictionary(g => g.Key, g => g.Count()),
            DocumentsByContentType = documents
                .GroupBy(d => d.ContentType)
                .ToDictionary(g => g.Key, g => g.Count())
        };

        return stats;
    }
}
