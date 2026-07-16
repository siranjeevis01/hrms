using HRMS.Services.Employee.Application.DTOs;
using MediatR;

namespace HRMS.Services.Employee.Application.Queries.GetEmployeeHistory;

public class GetEmployeeHistoryQuery : IRequest<List<EmployeeHistoryDto>>
{
    public Guid EmployeeId { get; set; }
}
