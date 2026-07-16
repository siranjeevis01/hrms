using MediatR;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HRMS.Services.Organization.Application.DTOs;
using HRMS.Services.Organization.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Organization.Application.Queries.GetShifts;

public class GetShiftsQueryHandler : IRequestHandler<GetShiftsQuery, PagedResult<ShiftDto>>
{
    private readonly IOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public GetShiftsQueryHandler(IOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<ShiftDto>> Handle(GetShiftsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Shifts
            .AsNoTracking()
            .AsQueryable();

        if (request.CompanyId.HasValue)
            query = query.Where(s => s.CompanyId == request.CompanyId.Value);

        if (request.IsActive.HasValue)
            query = query.Where(s => s.IsActive == request.IsActive.Value);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchTerm = request.SearchTerm.ToLower();
            query = query.Where(s =>
                s.Name.ToLower().Contains(searchTerm) ||
                s.Code.ToLower().Contains(searchTerm));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(s => s.StartTime)
            .ThenBy(s => s.Name)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<ShiftDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new PagedResult<ShiftDto>(items, totalCount, request.Page, request.PageSize);
    }
}
