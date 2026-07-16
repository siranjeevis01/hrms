using HRMS.Services.Document.Application.DTOs;
using HRMS.Services.Document.Application.Interfaces;
using HRMS.Services.Document.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Document.Application.Queries.GetDocuments;

public class GetDocumentsQuery : IRequest<PagedResult<DocumentDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Guid? FolderId { get; set; }
    public DocumentStatus? Status { get; set; }
    public DocumentCategory? Category { get; set; }
    public string? SearchTerm { get; set; }
    public string? TenantId { get; set; }
}

public class GetDocumentsQueryHandler : IRequestHandler<GetDocumentsQuery, PagedResult<DocumentDto>>
{
    private readonly IDocumentDbContext _context;
    private readonly AutoMapper.IMapper _mapper;

    public GetDocumentsQueryHandler(IDocumentDbContext context, AutoMapper.IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<DocumentDto>> Handle(GetDocumentsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Documents
            .Where(d => !d.IsDeleted);

        if (!string.IsNullOrEmpty(request.TenantId))
            query = query.Where(d => d.TenantId == request.TenantId);

        if (request.FolderId.HasValue)
            query = query.Where(d => d.FolderId == request.FolderId.Value);

        if (request.Status.HasValue)
            query = query.Where(d => d.Status == request.Status.Value);

        if (request.Category.HasValue)
            query = query.Where(d => d.Category == request.Category.Value);

        if (!string.IsNullOrEmpty(request.SearchTerm))
            query = query.Where(d => d.Name.Contains(request.SearchTerm) || d.FileName.Contains(request.SearchTerm));

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
