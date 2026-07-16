using HRMS.Services.Attendance.Application.Events;
using HRMS.Services.Attendance.Application.Interfaces;
using HRMS.Services.Attendance.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Attendance.Application.Commands.CheckOut;

public class CheckOutCommandHandler : IRequestHandler<CheckOutCommand, Unit>
{
    private readonly IAttendanceDbContext _context;
    private readonly IAttendanceRepository _repository;

    public CheckOutCommandHandler(IAttendanceDbContext context, IAttendanceRepository repository)
    {
        _context = context;
        _repository = repository;
    }

    public async Task<Unit> Handle(CheckOutCommand request, CancellationToken cancellationToken)
    {
        var today = DateTime.UtcNow.Date;

        var record = await _repository.GetByEmployeeAndDateAsync(
            request.EmployeeId, today, cancellationToken);

        if (record == null)
            throw new InvalidOperationException("No attendance record found for today. Please check in first.");

        if (!record.CheckInTime.HasValue)
            throw new InvalidOperationException("Check-in time is missing. Please check in first.");

        if (record.CheckOutTime.HasValue)
            throw new InvalidOperationException("Employee has already checked out today.");

        record.CheckOut(request.Method, request.Latitude, request.Longitude);

        var policy = await _repository.GetPolicyByCompanyAsync(record.TenantId, cancellationToken);
        if (policy != null)
        {
            var shiftAssignment = await _repository.GetCurrentShiftAssignmentAsync(request.EmployeeId, cancellationToken);
            if (shiftAssignment != null && record.CheckOutTime.HasValue)
            {
                var expectedEndTime = new TimeSpan(18, 0, 0);
                var earlyMinutes = record.CalculateEarlyExit(DateTime.Today.Add(expectedEndTime));
                if (earlyMinutes > 0)
                {
                    record.MarkAsEarlyExit(earlyMinutes);
                }
            }

            if (policy.OvertimeEnabled && record.TotalHours.HasValue)
            {
                var overtime = record.CalculateOvertimeHours(policy.OvertimeThresholdMinutes);
                if (overtime > policy.MaxOvertimeMinutes / 60m)
                    overtime = policy.MaxOvertimeMinutes / 60m;

                record.SetOvertimeHours(overtime);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
