using MediatR;

namespace HRMS.Services.Payroll.Application.Commands.GeneratePayslip;

public class GeneratePayslipCommand : IRequest<Guid>
{
    public Guid EmployeePayrollId { get; set; }
    public Guid EmployeeId { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public string PdfUrl { get; set; } = string.Empty;
    public Guid TenantId { get; set; }
}
