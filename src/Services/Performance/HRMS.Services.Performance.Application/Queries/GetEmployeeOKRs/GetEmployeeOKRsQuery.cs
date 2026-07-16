using HRMS.Services.Performance.Application.DTOs;
using MediatR;

namespace HRMS.Services.Performance.Application.Queries.GetEmployeeOKRs;

public class GetEmployeeOKRsQuery : IRequest<List<OKRDto>>
{
    public Guid EmployeeId { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
