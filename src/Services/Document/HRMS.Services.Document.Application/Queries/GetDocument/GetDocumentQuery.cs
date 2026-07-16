using HRMS.Services.Document.Application.DTOs;
using HRMS.Services.Document.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Document.Application.Queries.GetDocument;

public class GetDocumentQuery : IRequest<DocumentDto?>
{
    public Guid Id { get; set; }
}

public class GetDocumentQueryHandler : IRequestHandler<GetDocumentQuery, DocumentDto?>
{
    private readonly IDocumentDbContext _context;
    private readonly AutoMapper.IMapper _mapper;

    public GetDocumentQueryHandler(IDocumentDbContext context, AutoMapper.IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DocumentDto?> Handle(GetDocumentQuery request, CancellationToken cancellationToken)
    {
        var document = await _context.Documents
            .FirstOrDefaultAsync(d => d.Id == request.Id && !d.IsDeleted, cancellationToken);

        if (document == null) return null;

        return _mapper.Map<DocumentDto>(document);
    }
}
