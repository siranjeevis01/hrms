using AutoMapper;
using HRMS.Services.Payroll.Application.DTOs;
using HRMS.Services.Payroll.Application.Interfaces;
using HRMS.Services.Payroll.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Payroll.Application.Queries.GetEmployeePayroll;

public class GetEmployeePayrollQueryHandler : IRequestHandler<GetEmployeePayrollQuery, EmployeePayrollDto?>
{
    private readonly IPayrollDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeePayrollQueryHandler(IPayrollDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<EmployeePayrollDto?> Handle(GetEmployeePayrollQuery request, CancellationToken cancellationToken)
    {
        var employeePayroll = await _context.EmployeePayrolls
            .Include(ep => ep.Allowances)
            .Include(ep => ep.Deductions)
            .Include(ep => ep.LoanRepayments)
            .FirstOrDefaultAsync(ep => ep.EmployeeId == request.EmployeeId
                && ep.PayrollRun.Month == request.Month
                && ep.PayrollRun.Year == request.Year
                && ep.PayrollRun.Status != PayrollStatus.Reversed, cancellationToken);

        return employeePayroll == null ? null : _mapper.Map<EmployeePayrollDto>(employeePayroll);
    }
}
