using HRMS.Services.Payroll.Application.DTOs;
using MediatR;

namespace HRMS.Services.Payroll.Application.Queries.GetPayrollSummary;

public class GetPayrollSummaryQuery : IRequest<PayrollSummaryDto>
{
    public int Month { get; set; }
    public int Year { get; set; }
    public Guid TenantId { get; set; }
}
