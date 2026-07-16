using AutoMapper;
using HRMS.Services.Payroll.Application.DTOs;
using HRMS.Services.Payroll.Application.Interfaces;
using HRMS.Services.Payroll.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Payroll.Application.Queries.GetEmployeeSalaryBreakdown;

public class GetEmployeeSalaryBreakdownQueryHandler : IRequestHandler<GetEmployeeSalaryBreakdownQuery, SalaryBreakdownDto?>
{
    private readonly IPayrollDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeeSalaryBreakdownQueryHandler(IPayrollDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SalaryBreakdownDto?> Handle(GetEmployeeSalaryBreakdownQuery request, CancellationToken cancellationToken)
    {
        var components = await _context.SalaryComponentDefs
            .Where(c => c.IsActive && c.TenantId == request.TenantId)
            .OrderBy(c => c.SortOrder)
            .ToListAsync(cancellationToken);

        var assignments = await _context.EmployeeSalaryAssignments
            .Where(a => a.EmployeeId == request.EmployeeId && a.IsCurrent)
            .ToListAsync(cancellationToken);

        var latestPayroll = await _context.EmployeePayrolls
            .Where(ep => ep.EmployeeId == request.EmployeeId)
            .OrderByDescending(ep => ep.PayrollRun.Year)
            .ThenByDescending(ep => ep.PayrollRun.Month)
            .FirstOrDefaultAsync(cancellationToken);

        var breakdown = new SalaryBreakdownDto
        {
            EmployeeId = request.EmployeeId,
            BasicSalary = latestPayroll?.BasicSalary ?? 0,
            GrossSalary = latestPayroll?.GrossSalary ?? 0,
            TotalEarnings = latestPayroll?.TotalEarnings ?? 0,
            TotalDeductions = latestPayroll?.TotalDeductions ?? 0,
            NetSalary = latestPayroll?.NetPayable ?? 0
        };

        foreach (var component in components)
        {
            var assignment = assignments.FirstOrDefault(a => a.ComponentDefId == component.Id);
            var amount = assignment?.Amount ?? component.DefaultValue;

            if (component.CalculationType == CalculationType.Percentage)
            {
                var pct = assignment?.Percentage ?? component.DefaultValue;
                amount = (latestPayroll?.BasicSalary ?? 0) * pct / 100;
            }

            var item = new SalaryComponentItemDto
            {
                ComponentDefId = component.Id,
                Name = component.Name,
                Code = component.Code,
                Amount = amount,
                Percentage = assignment?.Percentage,
                IsTaxable = component.IsTaxable
            };

            if (component.Type == ComponentType.Earning)
                breakdown.Earnings.Add(item);
            else
                breakdown.Deductions.Add(item);
        }

        return breakdown;
    }
}
