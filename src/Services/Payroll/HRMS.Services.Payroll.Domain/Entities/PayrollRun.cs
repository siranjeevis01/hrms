using HRMS.Services.Payroll.Domain.Enums;
using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Payroll.Domain.Entities;

public class PayrollRun : AggregateRoot
{
    public Guid CompanyId { get; private set; }
    public int Month { get; private set; }
    public int Year { get; private set; }
    public new PayrollStatus Status { get; private set; }
    public DateTime? ProcessedAt { get; private set; }
    public Guid? ProcessedBy { get; private set; }
    public DateTime? ApprovedAt { get; private set; }
    public Guid? ApprovedBy { get; private set; }
    public int TotalEmployees { get; private set; }
    public decimal TotalGrossAmount { get; private set; }
    public decimal TotalDeductions { get; private set; }
    public decimal TotalNetAmount { get; private set; }
    public new Guid TenantId { get; private set; }

    private readonly List<EmployeePayroll> _employeePayrolls = new();
    public IReadOnlyCollection<EmployeePayroll> EmployeePayrolls => _employeePayrolls.AsReadOnly();

    private readonly List<PayrollAuditLog> _auditLogs = new();
    public IReadOnlyCollection<PayrollAuditLog> AuditLogs => _auditLogs.AsReadOnly();

    private PayrollRun() { }

    public PayrollRun(Guid companyId, int month, int year, Guid tenantId)
    {
        Id = Guid.NewGuid();
        CompanyId = companyId;
        Month = month;
        Year = year;
        Status = PayrollStatus.Draft;
        TenantId = tenantId;
        CreatedAt = DateTime.UtcNow;
    }

    public void StartProcessing(Guid processedBy)
    {
        if (Status != PayrollStatus.Draft)
            throw new InvalidOperationException($"Payroll in {Status} status cannot be processed.");

        Status = PayrollStatus.Processing;
        ProcessedBy = processedBy;
        AddAuditLog("Processing Started", processedBy.ToString(), null, "Payroll processing initiated.");
    }

    public void MarkProcessed(int totalEmployees, decimal totalGross, decimal totalDeductions, decimal totalNet, Guid processedBy)
    {
        if (Status != PayrollStatus.Processing)
            throw new InvalidOperationException("Payroll must be in Processing status to mark as processed.");

        Status = PayrollStatus.Processed;
        ProcessedAt = DateTime.UtcNow;
        ProcessedBy = processedBy;
        TotalEmployees = totalEmployees;
        TotalGrossAmount = totalGross;
        TotalDeductions = totalDeductions;
        TotalNetAmount = totalNet;
        AddAuditLog("Processing Completed", processedBy.ToString(), null,
            $"Processed {totalEmployees} employees. Gross: {totalGross}, Deductions: {totalDeductions}, Net: {totalNet}");
    }

    public void Approve(Guid approvedBy)
    {
        if (Status != PayrollStatus.Processed)
            throw new InvalidOperationException("Payroll must be Processed before approval.");

        Status = PayrollStatus.Approved;
        ApprovedAt = DateTime.UtcNow;
        ApprovedBy = approvedBy;
        AddAuditLog("Payroll Approved", approvedBy.ToString(), PayrollStatus.Processed.ToString(), PayrollStatus.Approved.ToString());
    }

    public void Lock(Guid lockedBy)
    {
        if (Status != PayrollStatus.Approved)
            throw new InvalidOperationException("Payroll must be Approved before locking.");

        Status = PayrollStatus.Locked;
        AddAuditLog("Payroll Locked", lockedBy.ToString(), PayrollStatus.Approved.ToString(), PayrollStatus.Locked.ToString());
    }

    public void MarkPaid(Guid paidBy)
    {
        if (Status != PayrollStatus.Approved && Status != PayrollStatus.Locked)
            throw new InvalidOperationException("Payroll must be Approved or Locked to mark as paid.");

        Status = PayrollStatus.Paid;
        AddAuditLog("Payroll Paid", paidBy.ToString(), null, "Payroll marked as paid.");
    }

    public void Reverse(Guid reversedBy, string reason)
    {
        if (Status != PayrollStatus.Processed && Status != PayrollStatus.Approved)
            throw new InvalidOperationException("Only Processed or Approved payrolls can be reversed.");

        var oldStatus = Status.ToString();
        Status = PayrollStatus.Reversed;
        AddAuditLog("Payroll Reversed", reversedBy.ToString(), oldStatus, reason);
    }

    public void AddEmployeePayroll(EmployeePayroll employeePayroll)
    {
        _employeePayrolls.Add(employeePayroll);
    }

    private void AddAuditLog(string action, string performedBy, string? oldValue, string? newValue)
    {
        _auditLogs.Add(new PayrollAuditLog(Id, action, performedBy, oldValue, newValue));
    }
}
