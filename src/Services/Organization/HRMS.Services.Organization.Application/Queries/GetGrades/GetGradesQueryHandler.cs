using MediatR;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HRMS.Services.Organization.Application.DTOs;
using HRMS.Services.Organization.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Organization.Application.Queries.GetGrades;

public class GetGradesQueryHandler : IRequestHandler<GetGradesQuery, PagedResult<GradeDto>>
{
    private readonly IOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public GetGradesQueryHandler(IOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<GradeDto>> Handle(GetGradesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Grades
            .AsNoTracking()
            .AsQueryable();

        if (request.CompanyId.HasValue)
            query = query.Where(g => g.CompanyId == request.CompanyId.Value);

        if (request.IsActive.HasValue)
            query = query.Where(g => g.IsActive == request.IsActive.Value);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchTerm = request.SearchTerm.ToLower();
            query = query.Where(g =>
                g.Name.ToLower().Contains(searchTerm) ||
                g.Code.ToLower().Contains(searchTerm));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(g => g.MinSalary)
            .ThenBy(g => g.Name)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<GradeDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new PagedResult<GradeDto>(items, totalCount, request.Page, request.PageSize);
    }
}
