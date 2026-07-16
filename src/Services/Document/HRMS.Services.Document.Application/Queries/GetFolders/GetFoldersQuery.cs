using HRMS.Services.Document.Application.DTOs;
using HRMS.Services.Document.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Document.Application.Queries.GetFolders;

public class GetFoldersQuery : IRequest<List<DocumentFolderDto>>
{
    public Guid? ParentFolderId { get; set; }
    public string? TenantId { get; set; }
}

public class GetFoldersQueryHandler : IRequestHandler<GetFoldersQuery, List<DocumentFolderDto>>
{
    private readonly IDocumentDbContext _context;
    private readonly AutoMapper.IMapper _mapper;

    public GetFoldersQueryHandler(IDocumentDbContext context, AutoMapper.IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DocumentFolderDto>> Handle(GetFoldersQuery request, CancellationToken cancellationToken)
    {
        var query = _context.DocumentFolders
            .Where(f => !f.IsDeleted);

        if (request.ParentFolderId.HasValue)
            query = query.Where(f => f.ParentFolderId == request.ParentFolderId.Value);
        else
            query = query.Where(f => f.ParentFolderId == null);

        if (!string.IsNullOrEmpty(request.TenantId))
            query = query.Where(f => f.TenantId == request.TenantId);

        var folders = await query
            .OrderBy(f => f.Name)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<DocumentFolderDto>>(folders);
    }
}
