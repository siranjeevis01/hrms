using AutoMapper;
using AutoMapper.QueryableExtensions;
using HRMS.Services.Payroll.Application.DTOs;
using HRMS.Services.Payroll.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Payroll.Application.Queries.GetEmployeePayrollHistory;

public class GetEmployeePayrollHistoryQueryHandler : IRequestHandler<GetEmployeePayrollHistoryQuery, List<EmployeePayrollDto>>
{
    private readonly IPayrollDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeePayrollHistoryQueryHandler(IPayrollDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<EmployeePayrollDto>> Handle(GetEmployeePayrollHistoryQuery request, CancellationToken cancellationToken)
    {
        var query = _context.EmployeePayrolls
            .Include(ep => ep.Allowances)
            .Include(ep => ep.Deductions)
            .Where(ep => ep.EmployeeId == request.EmployeeId);

        if (request.FromYear.HasValue)
            query = query.Where(ep => ep.PayrollRun.Year >= request.FromYear.Value);
        if (request.ToYear.HasValue)
            query = query.Where(ep => ep.PayrollRun.Year <= request.ToYear.Value);

        query = query.OrderByDescending(ep => ep.PayrollRun.Year)
            .ThenByDescending(ep => ep.PayrollRun.Month);

        return await query.ProjectTo<EmployeePayrollDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
