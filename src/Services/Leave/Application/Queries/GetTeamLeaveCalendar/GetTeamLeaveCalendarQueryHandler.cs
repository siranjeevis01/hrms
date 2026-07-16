using AutoMapper;
using HRMS.Services.Leave.Application.DTOs;
using HRMS.Services.Leave.Application.Interfaces;
using HRMS.Services.Leave.Domain.Enums;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Leave.Application.Queries.GetTeamLeaveCalendar;

public class GetTeamLeaveCalendarQueryHandler : IRequestHandler<GetTeamLeaveCalendarQuery, List<TeamLeaveCalendarDto>>
{
    private readonly ILeaveDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetTeamLeaveCalendarQueryHandler(ILeaveDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<List<TeamLeaveCalendarDto>> Handle(GetTeamLeaveCalendarQuery request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId ?? _currentUserService.TenantId ?? Guid.Empty;
        var monthStart = new DateTime(request.Month.Year, request.Month.Month, 1);
        var monthEnd = monthStart.AddMonths(1).AddDays(-1);

        var teamLeaves = await _context.LeaveApplications
            .Where(la => la.TenantId == tenantId
                && la.StartDate <= monthEnd && la.EndDate >= monthStart
                && (la.Status == LeaveStatus.Approved || la.Status == LeaveStatus.Pending))
            .ToListAsync(cancellationToken);

        var holidays = await _context.HolidayCalendarEntries
            .Where(h => h.Date >= monthStart && h.Date <= monthEnd && h.TenantId == tenantId)
            .ToListAsync(cancellationToken);

        var leaveTypes = await _context.LeaveTypes
            .Where(lt => lt.TenantId == tenantId)
            .ToListAsync(cancellationToken);

        var weekendDays = new HashSet<DayOfWeek> { DayOfWeek.Saturday, DayOfWeek.Sunday };

        var employeeGroups = teamLeaves.GroupBy(la => la.EmployeeId);

        var result = new List<TeamLeaveCalendarDto>();

        foreach (var group in employeeGroups)
        {
            var calendar = new TeamLeaveCalendarDto
            {
                EmployeeId = group.Key
            };

            for (var date = monthStart; date <= monthEnd; date = date.AddDays(1))
            {
                var dayDto = new LeaveCalendarDayDto
                {
                    Date = date,
                    IsWeekend = weekendDays.Contains(date.DayOfWeek)
                };

                var holiday = holidays.FirstOrDefault(h => h.Date == date);
                if (holiday != null)
                {
                    dayDto.IsHoliday = true;
                    dayDto.HolidayName = holiday.Name;
                }

                var leave = group.FirstOrDefault(la => la.StartDate <= date && la.EndDate >= date);
                if (leave != null)
                {
                    dayDto.IsLeave = true;
                    dayDto.IsHalfDay = leave.IsHalfDay && leave.StartDate == date;
                    dayDto.Status = leave.Status.ToString();

                    var lt = leaveTypes.FirstOrDefault(x => x.Id == leave.LeaveTypeId);
                    if (lt != null)
                    {
                        dayDto.LeaveTypeName = lt.Name;
                        dayDto.LeaveTypeColor = lt.Color;
                    }
                }

                calendar.Days.Add(dayDto);
            }

            result.Add(calendar);
        }

        return result;
    }
}
