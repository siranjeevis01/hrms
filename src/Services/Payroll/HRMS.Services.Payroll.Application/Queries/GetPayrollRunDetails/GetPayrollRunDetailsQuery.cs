using HRMS.Services.Payroll.Application.DTOs;
using MediatR;

namespace HRMS.Services.Payroll.Application.Queries.GetPayrollRunDetails;

public class GetPayrollRunDetailsQuery : IRequest<PayrollRunDto?>
{
    public Guid PayrollRunId { get; set; }
}
