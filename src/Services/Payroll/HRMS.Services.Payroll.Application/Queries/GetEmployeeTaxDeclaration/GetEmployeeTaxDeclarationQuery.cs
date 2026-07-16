using HRMS.Services.Payroll.Application.DTOs;
using MediatR;

namespace HRMS.Services.Payroll.Application.Queries.GetEmployeeTaxDeclaration;

public class GetEmployeeTaxDeclarationQuery : IRequest<EmployeeTaxDeclarationDto?>
{
    public Guid EmployeeId { get; set; }
    public string FinancialYear { get; set; } = string.Empty;
}
