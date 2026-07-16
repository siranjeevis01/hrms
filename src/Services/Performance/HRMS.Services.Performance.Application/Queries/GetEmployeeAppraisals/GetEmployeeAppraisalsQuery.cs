using HRMS.Services.Performance.Application.DTOs;
using MediatR;

namespace HRMS.Services.Performance.Application.Queries.GetEmployeeAppraisals;

public class GetEmployeeAppraisalsQuery : IRequest<List<AppraisalDto>>
{
    public Guid EmployeeId { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
