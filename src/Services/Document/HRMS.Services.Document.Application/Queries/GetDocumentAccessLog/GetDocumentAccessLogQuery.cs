using HRMS.Services.Document.Application.DTOs;
using HRMS.Services.Document.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Document.Application.Queries.GetDocumentAccessLog;

public class GetDocumentAccessLogQuery : IRequest<PagedResult<DocumentAccessLogDto>>
{
    public Guid? DocumentId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetDocumentAccessLogQueryHandler : IRequestHandler<GetDocumentAccessLogQuery, PagedResult<DocumentAccessLogDto>>
{
    private readonly IDocumentDbContext _context;
    private readonly AutoMapper.IMapper _mapper;

    public GetDocumentAccessLogQueryHandler(IDocumentDbContext context, AutoMapper.IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<DocumentAccessLogDto>> Handle(GetDocumentAccessLogQuery request, CancellationToken cancellationToken)
    {
        var query = _context.DocumentAccessLogs.AsQueryable();

        if (request.DocumentId.HasValue)
            query = query.Where(l => l.DocumentId == request.DocumentId.Value);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(l => l.AccessedAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<DocumentAccessLogDto>
        {
            Items = _mapper.Map<List<DocumentAccessLogDto>>(items),
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}
