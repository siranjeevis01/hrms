using AutoMapper;
using HRMS.Services.Payroll.Application.DTOs;
using HRMS.Services.Payroll.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Payroll.Application.Queries.GetPayrollRunDetails;

public class GetPayrollRunDetailsQueryHandler : IRequestHandler<GetPayrollRunDetailsQuery, PayrollRunDto?>
{
    private readonly IPayrollDbContext _context;
    private readonly IMapper _mapper;

    public GetPayrollRunDetailsQueryHandler(IPayrollDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PayrollRunDto?> Handle(GetPayrollRunDetailsQuery request, CancellationToken cancellationToken)
    {
        var payrollRun = await _context.PayrollRuns
            .Include(r => r.EmployeePayrolls)
                .ThenInclude(ep => ep.Allowances)
            .Include(r => r.EmployeePayrolls)
                .ThenInclude(ep => ep.Deductions)
            .Include(r => r.EmployeePayrolls)
                .ThenInclude(ep => ep.LoanRepayments)
            .FirstOrDefaultAsync(r => r.Id == request.PayrollRunId, cancellationToken);

        if (payrollRun == null) return null;

        var dto = _mapper.Map<PayrollRunDto>(payrollRun);
        dto.EmployeePayrolls = _mapper.Map<List<EmployeePayrollDto>>(payrollRun.EmployeePayrolls);
        return dto;
    }
}
