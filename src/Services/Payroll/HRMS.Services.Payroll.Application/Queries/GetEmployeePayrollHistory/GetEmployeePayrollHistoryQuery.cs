using HRMS.Services.Payroll.Application.DTOs;
using MediatR;

namespace HRMS.Services.Payroll.Application.Queries.GetEmployeePayrollHistory;

public class GetEmployeePayrollHistoryQuery : IRequest<List<EmployeePayrollDto>>
{
    public Guid EmployeeId { get; set; }
    public int? FromYear { get; set; }
    public int? ToYear { get; set; }
}
