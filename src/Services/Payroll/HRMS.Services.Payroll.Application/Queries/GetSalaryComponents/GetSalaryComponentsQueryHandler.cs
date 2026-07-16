using AutoMapper;
using AutoMapper.QueryableExtensions;
using HRMS.Services.Payroll.Application.DTOs;
using HRMS.Services.Payroll.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Payroll.Application.Queries.GetSalaryComponents;

public class GetSalaryComponentsQueryHandler : IRequestHandler<GetSalaryComponentsQuery, List<SalaryComponentDefDto>>
{
    private readonly IPayrollDbContext _context;
    private readonly IMapper _mapper;

    public GetSalaryComponentsQueryHandler(IPayrollDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<SalaryComponentDefDto>> Handle(GetSalaryComponentsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.SalaryComponentDefs
            .Where(c => c.TenantId == request.TenantId);

        if (request.ActiveOnly == true)
            query = query.Where(c => c.IsActive);

        query = query.OrderBy(c => c.SortOrder);

        return await query.ProjectTo<SalaryComponentDefDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
