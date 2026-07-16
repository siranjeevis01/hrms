using HRMS.Services.Document.Application.DTOs;
using HRMS.Services.Document.Application.Interfaces;
using HRMS.Services.Document.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Document.Application.Queries.SearchDocuments;

public class SearchDocumentsQuery : IRequest<PagedResult<DocumentDto>>
{
    public string SearchTerm { get; set; } = string.Empty;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public DocumentCategory? Category { get; set; }
    public string? TenantId { get; set; }
}

public class SearchDocumentsQueryHandler : IRequestHandler<SearchDocumentsQuery, PagedResult<DocumentDto>>
{
    private readonly IDocumentDbContext _context;
    private readonly AutoMapper.IMapper _mapper;

    public SearchDocumentsQueryHandler(IDocumentDbContext context, AutoMapper.IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<DocumentDto>> Handle(SearchDocumentsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Documents
            .Where(d => !d.IsDeleted && d.Status == DocumentStatus.Active);

        if (!string.IsNullOrEmpty(request.TenantId))
            query = query.Where(d => d.TenantId == request.TenantId);

        if (request.Category.HasValue)
            query = query.Where(d => d.Category == request.Category.Value);

        query = query.Where(d =>
            d.Name.Contains(request.SearchTerm) ||
            d.FileName.Contains(request.SearchTerm) ||
            (d.Description != null && d.Description.Contains(request.SearchTerm)) ||
            (d.Tags != null && d.Tags.Contains(request.SearchTerm)));

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(d => d.CreatedAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<DocumentDto>
        {
            Items = _mapper.Map<List<DocumentDto>>(items),
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}
