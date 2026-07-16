using HRMS.Services.Payroll.Domain.Entities;

namespace HRMS.Services.Payroll.Infrastructure.Services;

public class PayslipGenerator
{
    public PayslipData GeneratePayslipData(EmployeePayroll employeePayroll, PayrollRun payrollRun,
        EmployeeTaxDeclaration? taxDeclaration)
    {
        var data = new PayslipData
        {
            EmployeeId = employeePayroll.EmployeeId,
            EmployeePayrollId = employeePayroll.Id,
            Month = payrollRun.Month,
            Year = payrollRun.Year,
            BasicSalary = employeePayroll.BasicSalary,
            GrossSalary = employeePayroll.GrossSalary,
            TotalEarnings = employeePayroll.TotalEarnings,
            TotalDeductions = employeePayroll.TotalDeductions,
            NetPayable = employeePayroll.NetPayable,
            AttendanceDays = employeePayroll.AttendanceDays,
            LOPDays = employeePayroll.LOPDays,
            WorkingDays = employeePayroll.WorkingDays,
            PaidDays = employeePayroll.PaidDays,
            OvertimeHours = employeePayroll.OvertimeHours,
            OvertimeAmount = employeePayroll.OvertimeAmount,
            GeneratedAt = DateTime.UtcNow,
            Allowances = employeePayroll.Allowances.Select(a => new PayslipComponentItem
            {
                Name = a.Name,
                Amount = a.Amount,
                Type = "Earning"
            }).ToList(),
            Deductions = employeePayroll.Deductions.Select(d => new PayslipComponentItem
            {
                Name = d.Name,
                Amount = d.Amount,
                Type = "Deduction"
            }).ToList()
        };

        return data;
    }

    public byte[] GeneratePdf(PayslipData data)
    {
        // Placeholder for PDF generation using a library like QuestPDF, iTextSharp, etc.
        // Returns a simple text representation for now
        var lines = new List<string>
        {
            "PAYSLIP",
            $"Employee: {data.EmployeeId}",
            $"Period: {data.Month}/{data.Year}",
            $"Basic Salary: {data.BasicSalary:F2}",
            $"Gross Salary: {data.GrossSalary:F2}",
            $"Working Days: {data.WorkingDays}",
            $"Paid Days: {data.PaidDays}",
            $"LOP Days: {data.LOPDays}",
            $"Overtime Hours: {data.OvertimeHours}",
            $"Overtime Amount: {data.OvertimeAmount:F2}",
            "--- EARNINGS ---",
        };

        foreach (var allowance in data.Allowances)
        {
            lines.Add($"{allowance.Name}: {allowance.Amount:F2}");
        }

        lines.Add("--- DEDUCTIONS ---");
        foreach (var deduction in data.Deductions)
        {
            lines.Add($"{deduction.Name}: {deduction.Amount:F2}");
        }

        lines.Add($"NET PAYABLE: {data.NetPayable:F2}");

        return System.Text.Encoding.UTF8.GetBytes(string.Join(Environment.NewLine, lines));
    }
}

public class PayslipData
{
    public Guid EmployeeId { get; set; }
    public Guid EmployeePayrollId { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public decimal BasicSalary { get; set; }
    public decimal GrossSalary { get; set; }
    public decimal TotalEarnings { get; set; }
    public decimal TotalDeductions { get; set; }
    public decimal NetPayable { get; set; }
    public int AttendanceDays { get; set; }
    public int LOPDays { get; set; }
    public int WorkingDays { get; set; }
    public int PaidDays { get; set; }
    public decimal OvertimeHours { get; set; }
    public decimal OvertimeAmount { get; set; }
    public DateTime GeneratedAt { get; set; }
    public List<PayslipComponentItem> Allowances { get; set; } = new();
    public List<PayslipComponentItem> Deductions { get; set; } = new();
}

public class PayslipComponentItem
{
    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Type { get; set; } = string.Empty;
}
