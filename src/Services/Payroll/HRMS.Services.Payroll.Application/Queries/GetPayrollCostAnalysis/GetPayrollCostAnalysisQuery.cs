using HRMS.Services.Payroll.Application.DTOs;
using MediatR;

namespace HRMS.Services.Payroll.Application.Queries.GetPayrollCostAnalysis;

public class GetPayrollCostAnalysisQuery : IRequest<PayrollCostAnalysisDto>
{
    public int Year { get; set; }
    public int? MonthFrom { get; set; }
    public int? MonthTo { get; set; }
    public Guid TenantId { get; set; }
}
