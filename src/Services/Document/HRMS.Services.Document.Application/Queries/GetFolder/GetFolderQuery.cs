using HRMS.Services.Document.Application.DTOs;
using HRMS.Services.Document.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Document.Application.Queries.GetFolder;

public class GetFolderQuery : IRequest<DocumentFolderDto?>
{
    public Guid Id { get; set; }
}

public class GetFolderQueryHandler : IRequestHandler<GetFolderQuery, DocumentFolderDto?>
{
    private readonly IDocumentDbContext _context;
    private readonly AutoMapper.IMapper _mapper;

    public GetFolderQueryHandler(IDocumentDbContext context, AutoMapper.IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DocumentFolderDto?> Handle(GetFolderQuery request, CancellationToken cancellationToken)
    {
        var folder = await _context.DocumentFolders
            .FirstOrDefaultAsync(f => f.Id == request.Id && !f.IsDeleted, cancellationToken);

        if (folder == null) return null;

        return _mapper.Map<DocumentFolderDto>(folder);
    }
}
