using MediatR;
using HRMS.Services.Leave.Application.DTOs;

namespace HRMS.Services.Leave.Application.Queries.GetTeamLeaveCalendar;

public class GetTeamLeaveCalendarQuery : IRequest<List<TeamLeaveCalendarDto>>
{
    public Guid ManagerId { get; set; }
    public DateTime Month { get; set; }
    public Guid? TenantId { get; set; }
}
