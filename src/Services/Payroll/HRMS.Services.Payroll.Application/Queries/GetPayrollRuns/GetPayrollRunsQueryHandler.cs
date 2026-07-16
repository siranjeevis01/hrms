using AutoMapper;
using AutoMapper.QueryableExtensions;
using HRMS.Services.Payroll.Application.DTOs;
using HRMS.Services.Payroll.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Payroll.Application.Queries.GetPayrollRuns;

public class GetPayrollRunsQueryHandler : IRequestHandler<GetPayrollRunsQuery, List<PayrollRunDto>>
{
    private readonly IPayrollDbContext _context;
    private readonly IMapper _mapper;

    public GetPayrollRunsQueryHandler(IPayrollDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PayrollRunDto>> Handle(GetPayrollRunsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.PayrollRuns.AsQueryable();

        if (request.CompanyId.HasValue)
            query = query.Where(r => r.CompanyId == request.CompanyId.Value);
        if (request.Month.HasValue)
            query = query.Where(r => r.Month == request.Month.Value);
        if (request.Year.HasValue)
            query = query.Where(r => r.Year == request.Year.Value);

        query = query.Where(r => r.TenantId == request.TenantId);
        query = query.OrderByDescending(r => r.Year).ThenByDescending(r => r.Month);

        return await query.ProjectTo<PayrollRunDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
