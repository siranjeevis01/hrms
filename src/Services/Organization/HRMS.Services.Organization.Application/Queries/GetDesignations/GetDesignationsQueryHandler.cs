using MediatR;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HRMS.Services.Organization.Application.DTOs;
using HRMS.Services.Organization.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Organization.Application.Queries.GetDesignations;

public class GetDesignationsQueryHandler : IRequestHandler<GetDesignationsQuery, PagedResult<DesignationDto>>
{
    private readonly IOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public GetDesignationsQueryHandler(IOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<DesignationDto>> Handle(GetDesignationsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Designations
            .AsNoTracking()
            .AsQueryable();

        if (request.CompanyId.HasValue)
            query = query.Where(d => d.CompanyId == request.CompanyId.Value);

        if (request.IsActive.HasValue)
            query = query.Where(d => d.IsActive == request.IsActive.Value);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchTerm = request.SearchTerm.ToLower();
            query = query.Where(d =>
                d.Name.ToLower().Contains(searchTerm) ||
                d.Code.ToLower().Contains(searchTerm));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(d => d.Level)
            .ThenBy(d => d.Name)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<DesignationDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new PagedResult<DesignationDto>(items, totalCount, request.Page, request.PageSize);
    }
}
