using HRMS.Services.Payroll.Application.DTOs;
using MediatR;

namespace HRMS.Services.Payroll.Application.Queries.GetPayslip;

public class GetPayslipQuery : IRequest<PayslipDto?>
{
    public Guid EmployeeId { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
}
