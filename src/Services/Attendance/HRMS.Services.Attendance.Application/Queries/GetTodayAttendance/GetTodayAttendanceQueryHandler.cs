using HRMS.Services.Attendance.Application.DTOs;
using HRMS.Services.Attendance.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Attendance.Application.Queries.GetTodayAttendance;

public class GetTodayAttendanceQueryHandler : IRequestHandler<GetTodayAttendanceQuery, TodayAttendanceDto?>
{
    private readonly IAttendanceDbContext _context;
    private readonly IAttendanceRepository _repository;

    public GetTodayAttendanceQueryHandler(IAttendanceDbContext context, IAttendanceRepository repository)
    {
        _context = context;
        _repository = repository;
    }

    public async Task<TodayAttendanceDto?> Handle(GetTodayAttendanceQuery request, CancellationToken cancellationToken)
    {
        var today = DateTime.UtcNow.Date;

        var record = await _repository.GetByEmployeeAndDateAsync(
            request.EmployeeId, today, cancellationToken);

        var shiftAssignment = await _repository.GetCurrentShiftAssignmentAsync(
            request.EmployeeId, cancellationToken);

        var dto = new TodayAttendanceDto
        {
            EmployeeId = request.EmployeeId,
            Date = today,
            HasCheckedIn = record?.CheckInTime.HasValue ?? false,
            HasCheckedOut = record?.CheckOutTime.HasValue ?? false,
            CheckInTime = record?.CheckInTime,
            CheckOutTime = record?.CheckOutTime,
            CheckInMethod = record?.CheckInMethod,
            CheckOutMethod = record?.CheckOutMethod,
            Status = record?.Status ?? Domain.Enums.AttendanceStatus.Absent,
            TotalHoursWorked = record?.TotalHours,
            BreakMinutes = record?.BreakMinutes ?? 0,
            IsLate = record?.IsLate ?? false,
            LateMinutes = record?.LateMinutes ?? 0,
            IsEarlyExit = record?.IsEarlyExit ?? false,
            EarlyExitMinutes = record?.EarlyExitMinutes ?? 0,
            ShiftId = shiftAssignment?.ShiftId,
            Notes = record?.Notes
        };

        return dto;
    }
}
