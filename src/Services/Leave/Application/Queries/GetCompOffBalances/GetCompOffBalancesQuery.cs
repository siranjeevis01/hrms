using MediatR;
using HRMS.Services.Leave.Application.DTOs;

namespace HRMS.Services.Leave.Application.Queries.GetCompOffBalances;

public class GetCompOffBalancesQuery : IRequest<List<CompOffDto>>
{
    public Guid EmployeeId { get; set; }
    public string? Status { get; set; }
    public Guid? TenantId { get; set; }
}
