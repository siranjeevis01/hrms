using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Payroll.Domain.Entities;

public class EmployeePayroll : BaseEntity
{
    public Guid Id { get; private set; }
    public Guid PayrollRunId { get; private set; }
    public Guid EmployeeId { get; private set; }
    public Guid DepartmentId { get; private set; }
    public Guid DesignationId { get; private set; }
    public decimal BasicSalary { get; private set; }
    public decimal GrossSalary { get; private set; }
    public decimal TotalEarnings { get; private set; }
    public decimal TotalDeductions { get; private set; }
    public decimal NetPayable { get; private set; }
    public int AttendanceDays { get; private set; }
    public int LOPDays { get; private set; }
    public int WorkingDays { get; private set; }
    public int PaidDays { get; private set; }
    public decimal OvertimeHours { get; private set; }
    public decimal OvertimeAmount { get; private set; }
    public bool IsProcessed { get; private set; }
    public DateTime? ProcessedAt { get; private set; }
    public Guid TenantId { get; private set; }

    public PayrollRun PayrollRun { get; private set; } = null!;

    private readonly List<Allowance> _allowances = new();
    public IReadOnlyCollection<Allowance> Allowances => _allowances.AsReadOnly();

    private readonly List<Deduction> _deductions = new();
    public IReadOnlyCollection<Deduction> Deductions => _deductions.AsReadOnly();

    private readonly List<LoanRepayment> _loanRepayments = new();
    public IReadOnlyCollection<LoanRepayment> LoanRepayments => _loanRepayments.AsReadOnly();

    private EmployeePayroll() { }

    public EmployeePayroll(
        Guid payrollRunId, Guid employeeId, Guid departmentId, Guid designationId,
        decimal basicSalary, int attendanceDays, int lopDays, int workingDays,
        decimal overtimeHours, Guid tenantId)
    {
        Id = Guid.NewGuid();
        PayrollRunId = payrollRunId;
        EmployeeId = employeeId;
        DepartmentId = departmentId;
        DesignationId = designationId;
        BasicSalary = basicSalary;
        AttendanceDays = attendanceDays;
        LOPDays = lopDays;
        WorkingDays = workingDays;
        PaidDays = workingDays - lopDays;
        OvertimeHours = overtimeHours;
        TenantId = tenantId;
        CreatedAt = DateTime.UtcNow;
    }

    public void SetGrossSalary(decimal grossSalary)
    {
        GrossSalary = grossSalary;
    }

    public void SetEarnings(decimal totalEarnings)
    {
        TotalEarnings = totalEarnings;
    }

    public void SetDeductions(decimal totalDeductions)
    {
        TotalDeductions = totalDeductions;
    }

    public void SetOvertime(decimal overtimeAmount)
    {
        OvertimeAmount = overtimeAmount;
    }

    public void CalculateNetPayable()
    {
        NetPayable = TotalEarnings + OvertimeAmount - TotalDeductions;
        if (NetPayable < 0) NetPayable = 0;
    }

    public void MarkProcessed()
    {
        IsProcessed = true;
        ProcessedAt = DateTime.UtcNow;
    }

    public void AddAllowance(Allowance allowance)
    {
        _allowances.Add(allowance);
    }

    public void AddDeduction(Deduction deduction)
    {
        _deductions.Add(deduction);
    }

    public void AddLoanRepayment(LoanRepayment repayment)
    {
        _loanRepayments.Add(repayment);
    }
}
