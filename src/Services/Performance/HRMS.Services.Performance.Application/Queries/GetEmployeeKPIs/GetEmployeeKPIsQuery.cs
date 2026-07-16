using HRMS.Services.Performance.Application.DTOs;
using MediatR;

namespace HRMS.Services.Performance.Application.Queries.GetEmployeeKPIs;

public class GetEmployeeKPIsQuery : IRequest<List<KPIDto>>
{
    public Guid EmployeeId { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
