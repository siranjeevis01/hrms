using HRMS.Services.Document.Application.DTOs;
using HRMS.Services.Document.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Document.Application.Queries.GetDocumentVersions;

public class GetDocumentVersionsQuery : IRequest<List<DocumentVersionDto>>
{
    public Guid DocumentId { get; set; }
}

public class GetDocumentVersionsQueryHandler : IRequestHandler<GetDocumentVersionsQuery, List<DocumentVersionDto>>
{
    private readonly IDocumentDbContext _context;
    private readonly AutoMapper.IMapper _mapper;

    public GetDocumentVersionsQueryHandler(IDocumentDbContext context, AutoMapper.IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DocumentVersionDto>> Handle(GetDocumentVersionsQuery request, CancellationToken cancellationToken)
    {
        var versions = await _context.DocumentVersions
            .Where(v => v.DocumentId == request.DocumentId)
            .OrderByDescending(v => v.VersionNumber)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<DocumentVersionDto>>(versions);
    }
}
