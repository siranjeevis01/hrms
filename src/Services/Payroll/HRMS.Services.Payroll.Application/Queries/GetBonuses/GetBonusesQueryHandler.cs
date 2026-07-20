using AutoMapper;
using AutoMapper.QueryableExtensions;
using HRMS.Services.Payroll.Application.DTOs;
using HRMS.Services.Payroll.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Payroll.Application.Queries.GetBonuses;

public class GetBonusesQueryHandler : IRequestHandler<GetBonusesQuery, List<BonusDto>>
{
    private readonly IPayrollDbContext _context;
    private readonly IMapper _mapper;

    public GetBonusesQueryHandler(IPayrollDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<BonusDto>> Handle(GetBonusesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Bonuses.AsQueryable();

        if (request.EmployeeId.HasValue)
            query = query.Where(b => b.EmployeeId == request.EmployeeId.Value);
        if (request.Month.HasValue)
            query = query.Where(b => b.Month == request.Month.Value);
        if (request.Year.HasValue)
            query = query.Where(b => b.Year == request.Year.Value);

        query = query.OrderByDescending(b => b.Year).ThenByDescending(b => b.Month);

        return await query.ProjectTo<BonusDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
