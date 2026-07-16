using MediatR;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HRMS.Services.Organization.Application.DTOs;
using HRMS.Services.Organization.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Organization.Application.Queries.GetBranches;

public class GetBranchesQueryHandler : IRequestHandler<GetBranchesQuery, PagedResult<BranchDto>>
{
    private readonly IOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public GetBranchesQueryHandler(IOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<BranchDto>> Handle(GetBranchesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Branches
            .AsNoTracking()
            .AsQueryable();

        if (request.CompanyId.HasValue)
            query = query.Where(b => b.CompanyId == request.CompanyId.Value);

        if (request.IsActive.HasValue)
            query = query.Where(b => b.IsActive == request.IsActive.Value);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchTerm = request.SearchTerm.ToLower();
            query = query.Where(b =>
                b.Name.ToLower().Contains(searchTerm) ||
                b.Code.ToLower().Contains(searchTerm));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        query = request.SortBy?.ToLower() switch
        {
            "name" => request.SortDescending ? query.OrderByDescending(b => b.Name) : query.OrderBy(b => b.Name),
            "code" => request.SortDescending ? query.OrderByDescending(b => b.Code) : query.OrderBy(b => b.Code),
            "createdat" => request.SortDescending ? query.OrderByDescending(b => b.CreatedAt) : query.OrderBy(b => b.CreatedAt),
            _ => query.OrderBy(b => b.Name)
        };

        var items = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<BranchDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new PagedResult<BranchDto>(items, totalCount, request.Page, request.PageSize);
    }
}
