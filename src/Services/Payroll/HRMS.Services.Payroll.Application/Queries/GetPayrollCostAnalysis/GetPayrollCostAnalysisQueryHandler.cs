using HRMS.Services.Payroll.Application.DTOs;
using HRMS.Services.Payroll.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Payroll.Application.Queries.GetPayrollCostAnalysis;

public class GetPayrollCostAnalysisQueryHandler : IRequestHandler<GetPayrollCostAnalysisQuery, PayrollCostAnalysisDto>
{
    private readonly IPayrollDbContext _context;

    public GetPayrollCostAnalysisQueryHandler(IPayrollDbContext context)
    {
        _context = context;
    }

    public async Task<PayrollCostAnalysisDto> Handle(GetPayrollCostAnalysisQuery request, CancellationToken cancellationToken)
    {
        var query = _context.EmployeePayrolls
            .Include(ep => ep.PayrollRun)
            .Where(ep => ep.PayrollRun.Year == request.Year && ep.TenantId == request.TenantId);

        if (request.MonthFrom.HasValue)
            query = query.Where(ep => ep.PayrollRun.Month >= request.MonthFrom.Value);
        if (request.MonthTo.HasValue)
            query = query.Where(ep => ep.PayrollRun.Month <= request.MonthTo.Value);

        var employeePayrolls = await query.ToListAsync(cancellationToken);

        var result = new PayrollCostAnalysisDto();

        var monthlyGroups = employeePayrolls
            .GroupBy(ep => new { ep.PayrollRun.Month, ep.PayrollRun.Year })
            .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month);

        foreach (var group in monthlyGroups)
        {
            result.MonthlyTrends.Add(new MonthlyPayrollTrendDto
            {
                Month = group.Key.Month,
                Year = group.Key.Year,
                GrossPay = group.Sum(ep => ep.GrossSalary),
                NetPay = group.Sum(ep => ep.NetPayable),
                Deductions = group.Sum(ep => ep.TotalDeductions),
                EmployeeCount = group.Count()
            });
        }

        var departmentGroups = employeePayrolls
            .GroupBy(ep => ep.DepartmentId);

        decimal totalCost = employeePayrolls.Sum(ep => ep.GrossSalary);
        int totalEmployees = employeePayrolls.Select(ep => ep.EmployeeId).Distinct().Count();

        foreach (var group in departmentGroups)
        {
            var deptCost = group.Sum(ep => ep.GrossSalary);
            var deptEmployeeCount = group.Select(ep => ep.EmployeeId).Distinct().Count();

            result.DepartmentCosts.Add(new DepartmentCostDto
            {
                DepartmentId = group.Key,
                TotalCost = deptCost,
                PercentageOfTotal = totalCost > 0 ? Math.Round(deptCost / totalCost * 100, 2) : 0,
                AveragePerEmployee = deptEmployeeCount > 0 ? Math.Round(deptCost / deptEmployeeCount, 2) : 0,
                EmployeeCount = deptEmployeeCount
            });
        }

        result.TotalPayrollCost = totalCost;
        result.AverageCostPerEmployee = totalEmployees > 0 ? Math.Round(totalCost / totalEmployees, 2) : 0;

        return result;
    }
}
