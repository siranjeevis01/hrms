using HRMS.Services.Document.Application.DTOs;
using HRMS.Services.Document.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Document.Application.Queries.GetDocumentShares;

public class GetDocumentSharesQuery : IRequest<List<DocumentShareDto>>
{
    public Guid DocumentId { get; set; }
}

public class GetDocumentSharesQueryHandler : IRequestHandler<GetDocumentSharesQuery, List<DocumentShareDto>>
{
    private readonly IDocumentDbContext _context;
    private readonly AutoMapper.IMapper _mapper;

    public GetDocumentSharesQueryHandler(IDocumentDbContext context, AutoMapper.IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DocumentShareDto>> Handle(GetDocumentSharesQuery request, CancellationToken cancellationToken)
    {
        var shares = await _context.DocumentShares
            .Where(s => s.DocumentId == request.DocumentId && !s.IsDeleted)
            .OrderByDescending(s => s.SharedAt)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<DocumentShareDto>>(shares);
    }
}
