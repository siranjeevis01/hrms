using HRMS.Services.Payroll.Application.DTOs;
using HRMS.Services.Payroll.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Payroll.Application.Queries.GetPayrollSummary;

public class GetPayrollSummaryQueryHandler : IRequestHandler<GetPayrollSummaryQuery, PayrollSummaryDto>
{
    private readonly IPayrollDbContext _context;

    public GetPayrollSummaryQueryHandler(IPayrollDbContext context)
    {
        _context = context;
    }

    public async Task<PayrollSummaryDto> Handle(GetPayrollSummaryQuery request, CancellationToken cancellationToken)
    {
        var employeePayrolls = await _context.EmployeePayrolls
            .Include(ep => ep.PayrollRun)
            .Include(ep => ep.Allowances)
            .Include(ep => ep.Deductions)
            .Where(ep => ep.PayrollRun.Month == request.Month
                && ep.PayrollRun.Year == request.Year
                && ep.TenantId == request.TenantId)
            .ToListAsync(cancellationToken);

        var summary = new PayrollSummaryDto
        {
            Month = request.Month,
            Year = request.Year,
            TotalEmployees = employeePayrolls.Count,
            TotalGrossPay = employeePayrolls.Sum(ep => ep.GrossSalary),
            TotalDeductions = employeePayrolls.Sum(ep => ep.TotalDeductions),
            TotalNetPay = employeePayrolls.Sum(ep => ep.NetPayable),
            TotalPF = employeePayrolls.Sum(ep => ep.Deductions.Where(d => d.Name.Contains("Provident Fund")).Sum(d => d.Amount)),
            TotalESI = employeePayrolls.Sum(ep => ep.Deductions.Where(d => d.Name.Contains("ESI")).Sum(d => d.Amount)),
            TotalProfessionalTax = employeePayrolls.Sum(ep => ep.Deductions.Where(d => d.Name.Contains("Professional Tax")).Sum(d => d.Amount)),
            TotalLoanDeductions = employeePayrolls.Sum(ep => ep.Deductions.Where(d => d.Name.Contains("Loan")).Sum(d => d.Amount)),
            TotalOvertime = employeePayrolls.Sum(ep => ep.OvertimeAmount),
        };

        var departmentGroups = employeePayrolls
            .GroupBy(ep => new { ep.DepartmentId });

        foreach (var group in departmentGroups)
        {
            summary.DepartmentWise.Add(new DepartmentSummaryDto
            {
                DepartmentId = group.Key.DepartmentId,
                EmployeeCount = group.Count(),
                GrossPay = group.Sum(ep => ep.GrossSalary),
                Deductions = group.Sum(ep => ep.TotalDeductions),
                NetPay = group.Sum(ep => ep.NetPayable)
            });
        }

        return summary;
    }
}
