using AutoMapper;
using HRMS.Services.Payroll.Application.DTOs;
using HRMS.Services.Payroll.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Payroll.Application.Queries.GetPayslip;

public class GetPayslipQueryHandler : IRequestHandler<GetPayslipQuery, PayslipDto?>
{
    private readonly IPayrollDbContext _context;
    private readonly IMapper _mapper;

    public GetPayslipQueryHandler(IPayrollDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PayslipDto?> Handle(GetPayslipQuery request, CancellationToken cancellationToken)
    {
        var payslip = await _context.Payslips
            .Include(p => p.EmployeePayroll)
                .ThenInclude(ep => ep.Allowances)
            .Include(p => p.EmployeePayroll)
                .ThenInclude(ep => ep.Deductions)
            .FirstOrDefaultAsync(p => p.EmployeeId == request.EmployeeId
                && p.Month == request.Month && p.Year == request.Year, cancellationToken);

        if (payslip == null) return null;

        var dto = _mapper.Map<PayslipDto>(payslip);
        dto.PayrollDetails = _mapper.Map<EmployeePayrollDto>(payslip.EmployeePayroll);
        return dto;
    }
}
