using MediatR;
using HRMS.Services.Leave.Application.DTOs;

namespace HRMS.Services.Leave.Application.Queries.GetHolidays;

public class GetHolidaysQuery : IRequest<List<LeaveCalendarDayDto>>
{
    public Guid CompanyId { get; set; }
    public Guid? BranchId { get; set; }
    public int? Year { get; set; }
    public Guid? TenantId { get; set; }
}
