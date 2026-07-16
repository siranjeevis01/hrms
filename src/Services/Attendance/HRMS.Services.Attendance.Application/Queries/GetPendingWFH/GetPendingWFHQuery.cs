using HRMS.Services.Attendance.Application.DTOs;
using MediatR;

namespace HRMS.Services.Attendance.Application.Queries.GetPendingWFH;

public class GetPendingWFHQuery : IRequest<List<WorkFromHomeDto>>
{
    public Guid? TenantId { get; set; }
}
