using AutoMapper;
using HRMS.Services.Payroll.Application.Events;
using HRMS.Services.Payroll.Application.Interfaces;
using HRMS.Services.Payroll.Domain.Entities;
using HRMS.Services.Payroll.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Payroll.Application.Commands.ProcessPayroll;

public class ProcessPayrollCommandHandler : IRequestHandler<ProcessPayrollCommand, ProcessPayrollResult>
{
    private readonly IPayrollDbContext _context;
    private readonly IMapper _mapper;
    private readonly IPublisher _publisher;

    public ProcessPayrollCommandHandler(IPayrollDbContext context, IMapper mapper, IPublisher publisher)
    {
        _context = context;
        _mapper = mapper;
        _publisher = publisher;
    }

    public async Task<ProcessPayrollResult> Handle(ProcessPayrollCommand request, CancellationToken cancellationToken)
    {
        var existingRun = await _context.PayrollRuns
            .FirstOrDefaultAsync(r => r.CompanyId == request.CompanyId
                && r.Month == request.Month && r.Year == request.Year
                && r.Status != PayrollStatus.Reversed, cancellationToken);

        PayrollRun payrollRun;
        if (existingRun != null)
        {
            payrollRun = existingRun;
            payrollRun.StartProcessing(request.ProcessedBy);
        }
        else
        {
            payrollRun = new PayrollRun(request.CompanyId, request.Month, request.Year, request.TenantId);
            payrollRun.StartProcessing(request.ProcessedBy);
            _context.PayrollRuns.Add(payrollRun);
        }

        var salaryComponents = await _context.SalaryComponentDefs
            .Where(s => s.IsActive && s.TenantId == request.TenantId)
            .OrderBy(s => s.SortOrder)
            .ToListAsync(cancellationToken);

        var taxConfig = await _context.TaxConfigurations
            .FirstOrDefaultAsync(t => t.CompanyId == request.CompanyId
                && t.FinancialYear == $"{request.Year}-{(request.Year + 1) % 100:D2}"
                && t.TenantId == request.TenantId, cancellationToken);

        var activeLoans = await _context.Loans
            .Where(l => l.Status == LoanStatus.Active && l.TenantId == request.TenantId)
            .ToListAsync(cancellationToken);

        var assignedComponents = await _context.EmployeeSalaryAssignments
            .Where(a => a.IsCurrent && a.TenantId == request.TenantId)
            .Include(a => a.ComponentDef)
            .ToListAsync(cancellationToken);

        var taxDeclarations = await _context.EmployeeTaxDeclarations
            .Where(d => d.FinancialYear == $"{request.Year}-{(request.Year + 1) % 100:D2}"
                && d.Status == PayrollStatus.Approved)
            .ToListAsync(cancellationToken);

        var result = new ProcessPayrollResult();
        decimal totalGross = 0, totalDeductions = 0, totalNet = 0;

        foreach (var employeeInput in request.Employees)
        {
            var employeeResult = await ProcessEmployee(
                employeeInput, payrollRun.Id, salaryComponents, assignedComponents,
                taxConfig, activeLoans, taxDeclarations, request.TenantId, cancellationToken);

            totalGross += employeeResult.GrossSalary;
            totalDeductions += employeeResult.TotalDeductions;
            totalNet += employeeResult.NetPayable;
            result.EmployeeResults.Add(employeeResult);
        }

        payrollRun.MarkProcessed(
            request.Employees.Count, totalGross, totalDeductions, totalNet, request.ProcessedBy);

        await _context.SaveChangesAsync(cancellationToken);

        result.PayrollRunId = payrollRun.Id;
        result.EmployeeCount = request.Employees.Count;
        result.TotalGross = totalGross;
        result.TotalDeductions = totalDeductions;
        result.TotalNet = totalNet;

        await _publisher.Publish(new PayrollProcessedEvent(
            payrollRun.Id, request.CompanyId, request.Month, request.Year,
            request.Employees.Count, totalGross, totalNet, request.ProcessedBy), cancellationToken);

        return result;
    }

    private Task<EmployeePayrollResult> ProcessEmployee(
        EmployeePayrollInput input, Guid payrollRunId,
        List<SalaryComponentDef> salaryComponents,
        List<EmployeeSalaryAssignment> assignedComponents,
        TaxConfiguration? taxConfig,
        List<Loan> activeLoans,
        List<EmployeeTaxDeclaration> taxDeclarations,
        Guid tenantId,
        CancellationToken cancellationToken)
    {
        var paidDays = input.WorkingDays - input.LOPDays;
        var dayRate = input.BasicSalary / input.WorkingDays;
        var effectiveBasic = dayRate * paidDays;

        var employeePayroll = new EmployeePayroll(
            payrollRunId, input.EmployeeId, input.DepartmentId, input.DesignationId,
            effectiveBasic, input.AttendanceDays, input.LOPDays, input.WorkingDays,
            input.OvertimeHours, tenantId);

        var employeeResult = new EmployeePayrollResult
        {
            EmployeeId = input.EmployeeId,
            GrossSalary = effectiveBasic
        };

        var earnings = new List<Allowance>();
        var deductions = new List<Deduction>();
        decimal totalEarnings = effectiveBasic;
        decimal totalDeductions = 0;

        var employeeComponents = assignedComponents
            .Where(a => a.EmployeeId == input.EmployeeId)
            .ToList();

        foreach (var component in salaryComponents)
        {
            var assignment = employeeComponents.FirstOrDefault(a => a.ComponentDefId == component.Id);
            var compValue = assignment?.Amount ?? component.DefaultValue;

            decimal calculatedAmount;
            if (component.CalculationType == CalculationType.Percentage)
            {
                var baseValue = assignment?.Percentage ?? component.DefaultValue;
                calculatedAmount = effectiveBasic * (baseValue / 100);
            }
            else if (component.CalculationType == CalculationType.Formula)
            {
                calculatedAmount = CalculateFormula(component.Formula, effectiveBasic, compValue, dayRate, paidDays);
            }
            else
            {
                calculatedAmount = compValue;
            }

            if (component.Type == ComponentType.Earning)
            {
                var allowance = new Allowance(
                    employeePayroll.Id, component.Id, component.Name,
                    calculatedAmount, ComponentType.Earning, component.IsTaxable);
                earnings.Add(allowance);
                totalEarnings += calculatedAmount;
                employeeResult.Allowances.Add(new BreakdownItem { Name = component.Name, Amount = calculatedAmount });
            }
            else
            {
                var deduction = new Deduction(
                    employeePayroll.Id, component.Id, component.Name,
                    calculatedAmount, ComponentType.Deduction, component.IsPFApplicable || component.IsESIApplicable);
                deductions.Add(deduction);
                totalDeductions += calculatedAmount;
                employeeResult.Deductions.Add(new BreakdownItem { Name = component.Name, Amount = calculatedAmount });
            }
        }

        var overtimeAmount = input.OvertimeHours * input.OvertimeRate;
        if (overtimeAmount > 0)
        {
            employeePayroll.SetOvertime(overtimeAmount);
            employeeResult.OvertimeAmount = overtimeAmount;
            totalEarnings += overtimeAmount;
        }

        var taxableIncome = totalEarnings;
        foreach (var allowance in earnings.Where(a => a.IsTaxable))
            taxableIncome += allowance.Amount;

        var pfAmount = CalculatePF(effectiveBasic, totalEarnings, taxConfig);
        if (pfAmount > 0)
        {
            deductions.Add(new Deduction(employeePayroll.Id, Guid.Empty, "Provident Fund", pfAmount, ComponentType.Deduction, true));
            totalDeductions += pfAmount;
            employeeResult.PFAmount = pfAmount;
            employeeResult.Deductions.Add(new BreakdownItem { Name = "Provident Fund", Amount = pfAmount });
        }

        var esiAmount = CalculateESI(totalEarnings, taxConfig);
        if (esiAmount > 0)
        {
            deductions.Add(new Deduction(employeePayroll.Id, Guid.Empty, "ESI", esiAmount, ComponentType.Deduction, true));
            totalDeductions += esiAmount;
            employeeResult.ESIAmount = esiAmount;
            employeeResult.Deductions.Add(new BreakdownItem { Name = "ESI", Amount = esiAmount });
        }

        var professionalTax = CalculateProfessionalTax(totalEarnings, taxConfig);
        if (professionalTax > 0)
        {
            deductions.Add(new Deduction(employeePayroll.Id, Guid.Empty, "Professional Tax", professionalTax, ComponentType.Deduction, true));
            totalDeductions += professionalTax;
            employeeResult.ProfessionalTax = professionalTax;
            employeeResult.Deductions.Add(new BreakdownItem { Name = "Professional Tax", Amount = professionalTax });
        }

        var employeeLoan = activeLoans.FirstOrDefault(l => l.EmployeeId == input.EmployeeId);
        if (employeeLoan != null && employeeLoan.MonthlyDeduction > 0)
        {
            var loanAmount = Math.Min(employeeLoan.MonthlyDeduction, employeeLoan.OutstandingAmount);
            employeeLoan.DeductPayment(loanAmount, employeePayroll.Id);
            deductions.Add(new Deduction(employeePayroll.Id, Guid.Empty, $"Loan Repayment ({employeeLoan.LoanType})", loanAmount, ComponentType.Deduction, false));
            totalDeductions += loanAmount;
            employeeResult.LoanDeduction = loanAmount;
            employeeResult.Deductions.Add(new BreakdownItem { Name = $"Loan Repayment ({employeeLoan.LoanType})", Amount = loanAmount });
            employeePayroll.AddLoanRepayment(employeeLoan.Repayments.Last());
        }

        var incomeTax = CalculateIncomeTax(taxableIncome, taxConfig, taxDeclarations.FirstOrDefault(d => d.EmployeeId == input.EmployeeId));
        if (incomeTax > 0)
        {
            deductions.Add(new Deduction(employeePayroll.Id, Guid.Empty, "Income Tax", incomeTax, ComponentType.Deduction, true));
            totalDeductions += incomeTax;
            employeeResult.TaxDeducted = incomeTax;
            employeeResult.Deductions.Add(new BreakdownItem { Name = "Income Tax", Amount = incomeTax });
        }

        employeePayroll.SetGrossSalary(totalEarnings);
        employeePayroll.SetEarnings(totalEarnings);
        employeePayroll.SetDeductions(totalDeductions);
        employeePayroll.CalculateNetPayable();
        employeePayroll.MarkProcessed();

        foreach (var allowance in earnings)
            employeePayroll.AddAllowance(allowance);
        foreach (var deduction in deductions)
            employeePayroll.AddDeduction(deduction);

        _context.EmployeePayrolls.Add(employeePayroll);

        employeeResult.GrossSalary = totalEarnings;
        employeeResult.TotalEarnings = totalEarnings;
        employeeResult.TotalDeductions = totalDeductions;
        employeeResult.NetPayable = employeePayroll.NetPayable;

        return Task.FromResult(employeeResult);
    }

    private decimal CalculateFormula(string? formula, decimal basic, decimal compValue, decimal dayRate, int paidDays)
    {
        if (string.IsNullOrWhiteSpace(formula)) return 0;

        try
        {
            var expression = formula
                .Replace("Basic", basic.ToString("F2"))
                .Replace("Value", compValue.ToString("F2"))
                .Replace("DayRate", dayRate.ToString("F2"))
                .Replace("PaidDays", paidDays.ToString());

            var dt = new System.Data.DataTable();
            var result = dt.Compute(expression, string.Empty);
            return Convert.ToDecimal(result);
        }
        catch
        {
            return compValue;
        }
    }

    private decimal CalculatePF(decimal basic, decimal gross, TaxConfiguration? config)
    {
        var rate = config?.PFContributionRate ?? 12m;
        var pfWage = Math.Min(basic, 15000m);
        return Math.Round(pfWage * rate / 100, 2);
    }

    private decimal CalculateESI(decimal gross, TaxConfiguration? config)
    {
        var rate = config?.ESIContributionRate ?? 0.75m;
        if (gross <= 21000m)
            return Math.Round(gross * rate / 100, 2);
        return 0;
    }

    private decimal CalculateProfessionalTax(decimal gross, TaxConfiguration? config)
    {
        if (gross <= 10000m) return 0;
        if (gross <= 15000m) return 100;
        if (gross <= 25000m) return 150;
        return 200;
    }

    private decimal CalculateIncomeTax(decimal taxableIncome, TaxConfiguration? config,
        EmployeeTaxDeclaration? declaration)
    {
        if (config == null) return 0;

        var investmentDeduction = declaration?.InvestedAmount ?? 0;
        var standardDeduction = config.StandardDeduction > 0 ? config.StandardDeduction : 50000m;

        // Annualize monthly income
        var annualIncome = taxableIncome * 12;
        var deductions = investmentDeduction + standardDeduction;
        annualIncome = Math.Max(0, annualIncome - deductions);

        if (config.TaxSlabConfig == "[]" || string.IsNullOrEmpty(config.TaxSlabConfig))
            return 0;

        // Basic progressive tax calculation using standard slabs
        if (annualIncome <= 250000) return 0;
        decimal tax = 0;
        if (annualIncome > 250000 && annualIncome <= 500000)
            tax = (annualIncome - 250000) * 0.05m;
        else if (annualIncome > 500000 && annualIncome <= 1000000)
            tax = 12500 + (annualIncome - 500000) * 0.20m;
        else
            tax = 112500 + (annualIncome - 1000000) * 0.30m;

        var monthlyTax = Math.Round(tax / 12, 2);
        return monthlyTax;
    }
}
