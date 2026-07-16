using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Payroll.Domain.Entities;

public class Payslip : BaseEntity
{
    public Guid Id { get; private set; }
    public Guid EmployeePayrollId { get; private set; }
    public Guid EmployeeId { get; private set; }
    public int Month { get; private set; }
    public int Year { get; private set; }
    public string PdfUrl { get; private set; } = string.Empty;
    public DateTime GeneratedAt { get; private set; }
    public bool IsViewed { get; private set; }
    public DateTime? ViewedAt { get; private set; }
    public Guid TenantId { get; private set; }

    public EmployeePayroll EmployeePayroll { get; private set; } = null!;

    private Payslip() { }

    public Payslip(Guid employeePayrollId, Guid employeeId, int month, int year, string pdfUrl, Guid tenantId)
    {
        Id = Guid.NewGuid();
        EmployeePayrollId = employeePayrollId;
        EmployeeId = employeeId;
        Month = month;
        Year = year;
        PdfUrl = pdfUrl;
        GeneratedAt = DateTime.UtcNow;
        TenantId = tenantId;
        CreatedAt = DateTime.UtcNow;
    }

    public void MarkViewed()
    {
        IsViewed = true;
        ViewedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
