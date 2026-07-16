using HRMS.Services.Employee.Application.DTOs;
using MediatR;

namespace HRMS.Services.Employee.Application.Queries.GetNewHires;

public class GetNewHiresQuery : IRequest<List<EmployeeListDto>>
{
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public Guid? DepartmentId { get; set; }
}
