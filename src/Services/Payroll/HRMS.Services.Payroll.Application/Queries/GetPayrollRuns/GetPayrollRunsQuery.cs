using HRMS.Services.Payroll.Application.DTOs;
using MediatR;

namespace HRMS.Services.Payroll.Application.Queries.GetPayrollRuns;

public class GetPayrollRunsQuery : IRequest<List<PayrollRunDto>>
{
    public Guid? CompanyId { get; set; }
    public int? Month { get; set; }
    public int? Year { get; set; }
    public Guid TenantId { get; set; }
}
