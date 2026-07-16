using HRMS.Services.Document.Application.DTOs;
using HRMS.Services.Document.Application.Interfaces;
using HRMS.Services.Document.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Document.Application.Queries.GetFolderDocuments;

public class GetFolderDocumentsQuery : IRequest<PagedResult<DocumentDto>>
{
    public Guid FolderId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public DocumentStatus? Status { get; set; }
}

public class GetFolderDocumentsQueryHandler : IRequestHandler<GetFolderDocumentsQuery, PagedResult<DocumentDto>>
{
    private readonly IDocumentDbContext _context;
    private readonly AutoMapper.IMapper _mapper;

    public GetFolderDocumentsQueryHandler(IDocumentDbContext context, AutoMapper.IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<DocumentDto>> Handle(GetFolderDocumentsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Documents
            .Where(d => d.FolderId == request.FolderId && !d.IsDeleted);

        if (request.Status.HasValue)
            query = query.Where(d => d.Status == request.Status.Value);

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
