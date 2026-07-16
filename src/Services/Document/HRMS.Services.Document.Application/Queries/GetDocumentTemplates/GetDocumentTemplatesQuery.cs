using HRMS.Services.Document.Application.DTOs;
using HRMS.Services.Document.Application.Interfaces;
using HRMS.Services.Document.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Document.Application.Queries.GetDocumentTemplates;

public class GetDocumentTemplatesQuery : IRequest<PagedResult<DocumentTemplateDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public DocumentCategory? Category { get; set; }
    public string? SearchTerm { get; set; }
}

public class GetDocumentTemplatesQueryHandler : IRequestHandler<GetDocumentTemplatesQuery, PagedResult<DocumentTemplateDto>>
{
    private readonly IDocumentDbContext _context;
    private readonly AutoMapper.IMapper _mapper;

    public GetDocumentTemplatesQueryHandler(IDocumentDbContext context, AutoMapper.IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<DocumentTemplateDto>> Handle(GetDocumentTemplatesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.DocumentTemplates
            .Where(t => !t.IsDeleted);

        if (request.Category.HasValue)
            query = query.Where(t => t.Category == request.Category.Value);

        if (!string.IsNullOrEmpty(request.SearchTerm))
            query = query.Where(t => t.Name.Contains(request.SearchTerm));

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(t => t.CreatedAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<DocumentTemplateDto>
        {
            Items = _mapper.Map<List<DocumentTemplateDto>>(items),
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}
