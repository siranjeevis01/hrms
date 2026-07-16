using HRMS.Services.Leave.Application.DTOs;
using HRMS.Services.Leave.Application.Interfaces;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Leave.Application.Queries.GetHolidays;

public class GetHolidaysQueryHandler : IRequestHandler<GetHolidaysQuery, List<LeaveCalendarDayDto>>
{
    private readonly ILeaveDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetHolidaysQueryHandler(ILeaveDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<List<LeaveCalendarDayDto>> Handle(GetHolidaysQuery request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId ?? _currentUserService.TenantId ?? Guid.Empty;
        var year = request.Year ?? DateTime.UtcNow.Year;

        var startDate = new DateTime(year, 1, 1);
        var endDate = new DateTime(year, 12, 31);

        var holidays = await _context.HolidayCalendarEntries
            .Where(h => h.CompanyId == request.CompanyId && h.TenantId == tenantId
                && h.Date >= startDate && h.Date <= endDate)
            .OrderBy(h => h.Date)
            .ToListAsync(cancellationToken);

        if (request.BranchId.HasValue)
        {
            holidays = holidays.Where(h => !h.BranchId.HasValue || h.BranchId == request.BranchId.Value).ToList();
        }
        else
        {
            holidays = holidays.Where(h => !h.BranchId.HasValue).ToList();
        }

        var weekendDays = new HashSet<DayOfWeek> { DayOfWeek.Saturday, DayOfWeek.Sunday };

        var result = new List<LeaveCalendarDayDto>();

        for (var date = startDate; date <= endDate; date = date.AddDays(1))
        {
            var holiday = holidays.FirstOrDefault(h => h.Date == date);
            if (holiday != null)
            {
                result.Add(new LeaveCalendarDayDto
                {
                    Date = date,
                    IsHoliday = true,
                    HolidayName = holiday.Name,
                    IsWeekend = weekendDays.Contains(date.DayOfWeek)
                });
            }
        }

        return result;
    }
}
