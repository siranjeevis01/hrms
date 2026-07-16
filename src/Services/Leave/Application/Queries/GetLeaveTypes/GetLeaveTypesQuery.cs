using MediatR;
using HRMS.Services.Leave.Application.DTOs;

namespace HRMS.Services.Leave.Application.Queries.GetLeaveTypes;

public class GetLeaveTypesQuery : IRequest<List<LeaveTypeDto>>
{
    public bool? IncludeInactive { get; set; }
    public Guid? TenantId { get; set; }
}
