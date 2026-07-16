namespace HRMS.Services.Payroll.Application.DTOs;

public class PayslipDto
{
    public Guid Id { get; set; }
    public Guid EmployeePayrollId { get; set; }
    public Guid EmployeeId { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public string PdfUrl { get; set; } = string.Empty;
    public DateTime GeneratedAt { get; set; }
    public bool IsViewed { get; set; }
    public DateTime? ViewedAt { get; set; }
    public EmployeePayrollDto? PayrollDetails { get; set; }
}
