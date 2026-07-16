using HRMS.Services.Performance.Application.DTOs;
using MediatR;

namespace HRMS.Services.Performance.Application.Queries.GetFeedback360ByEmployee;

public class GetFeedback360ByEmployeeQuery : IRequest<List<Feedback360Dto>>
{
    public Guid EmployeeId { get; set; }
    public string TenantId { get; set; } = string.Empty;
}
