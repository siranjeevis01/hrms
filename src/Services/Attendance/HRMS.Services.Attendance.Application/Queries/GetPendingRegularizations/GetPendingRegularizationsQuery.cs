using HRMS.Services.Attendance.Application.DTOs;
using MediatR;

namespace HRMS.Services.Attendance.Application.Queries.GetPendingRegularizations;

public class GetPendingRegularizationsQuery : IRequest<List<RegularizationDto>>
{
    public Guid? TenantId { get; set; }
}
