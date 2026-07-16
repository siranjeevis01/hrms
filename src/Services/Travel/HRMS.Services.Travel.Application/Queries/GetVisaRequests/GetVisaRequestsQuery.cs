using HRMS.Services.Travel.Application.DTOs;
using MediatR;

namespace HRMS.Services.Travel.Application.Queries.GetVisaRequests;

public class GetVisaRequestsQuery : IRequest<List<VisaRequestDto>>
{
    public Guid? EmployeeId { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
