using MediatR;
using AutoMapper;
using HRMS.Services.Organization.Application.DTOs;
using HRMS.Services.Organization.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Organization.Application.Queries.GetHolidays;

public class GetHolidaysQueryHandler : IRequestHandler<GetHolidaysQuery, List<HolidayDto>>
{
    private readonly IOrganizationDbContext _context;
    private readonly IMapper _mapper;

    public GetHolidaysQueryHandler(IOrganizationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<HolidayDto>> Handle(GetHolidaysQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Holidays
            .AsNoTracking()
            .Where(h => h.CompanyId == request.CompanyId)
            .AsQueryable();

        if (request.Year.HasValue)
        {
            var startDate = new DateTime(request.Year.Value, 1, 1);
            var endDate = new DateTime(request.Year.Value, 12, 31);
            query = query.Where(h => h.Date >= startDate && h.Date <= endDate);
        }

        if (request.BranchId.HasValue)
            query = query.Where(h => h.BranchId == request.BranchId.Value || h.BranchId == null);

        if (request.IsActive.HasValue)
            query = query.Where(h => h.IsActive == request.IsActive.Value);

        var holidays = await query
            .OrderBy(h => h.Date)
            .ToListAsync(cancellationToken);

        var dtos = _mapper.Map<List<HolidayDto>>(holidays);

        foreach (var dto in dtos)
        {
            var holiday = holidays.First(h => h.Id == dto.Id);
            dto.CompanyName = string.Empty;
            dto.BranchName = null;
        }

        return dtos;
    }
}
