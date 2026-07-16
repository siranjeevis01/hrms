using HRMS.Services.Attendance.Application.Events;
using HRMS.Services.Attendance.Application.Interfaces;
using HRMS.Services.Attendance.Domain.Entities;
using HRMS.Services.Attendance.Domain.Enums;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Attendance.Application.Commands.CheckIn;

public class CheckInCommandHandler : IRequestHandler<CheckInCommand, Guid>
{
    private readonly IAttendanceDbContext _context;
    private readonly IAttendanceRepository _repository;
    private readonly ICurrentUserService _currentUserService;

    public CheckInCommandHandler(
        IAttendanceDbContext context,
        IAttendanceRepository repository,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _repository = repository;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CheckInCommand request, CancellationToken cancellationToken)
    {
        var today = DateTime.UtcNow.Date;

        var existingRecord = await _repository.GetByEmployeeAndDateAsync(
            request.EmployeeId, today, cancellationToken);

        if (existingRecord != null && existingRecord.CheckInTime.HasValue)
            throw new InvalidOperationException("Employee has already checked in today.");

        var tenantId = request.TenantId ?? _currentUserService.TenantId ?? Guid.Empty;

        if (request.Method == CheckInMethod.GPS && request.Latitude.HasValue && request.Longitude.HasValue)
        {
            var companyId = tenantId;
            var isInside = await _repository.IsWithinGeoFenceAsync(
                request.Latitude.Value, request.Longitude.Value, companyId, cancellationToken);

            if (!isInside)
                throw new InvalidOperationException("Check-in location is outside the allowed geo-fence area.");
        }

        if (request.Method == CheckInMethod.WiFi && !string.IsNullOrEmpty(request.WifiSSID))
        {
            var companyId = tenantId;
            var isValid = await _repository.IsValidWifiAsync(
                request.WifiSSID, request.WifiBSSID, companyId, cancellationToken);

            if (!isValid)
                throw new InvalidOperationException("The connected WiFi network is not recognized for attendance.");
        }

        var shiftAssignment = await _repository.GetCurrentShiftAssignmentAsync(
            request.EmployeeId, cancellationToken);

        var record = AttendanceRecord.CreateCheckIn(
            request.EmployeeId,
            today,
            shiftAssignment?.ShiftId,
            request.Method,
            request.Latitude,
            request.Longitude,
            request.WifiSSID,
            request.WifiBSSID,
            request.QrCodeId,
            tenantId);

        if (shiftAssignment != null)
        {
            var shift = await _context.AttendancePolicies
                .FirstOrDefaultAsync(p => p.CompanyId == tenantId, cancellationToken);

            if (shift != null && record.CheckInTime.HasValue)
            {
                var policy = await _repository.GetPolicyByCompanyAsync(tenantId, cancellationToken);
                if (policy != null)
                {
                    var now = DateTime.UtcNow.TimeOfDay;
                    var graceEnd = new TimeSpan(9, 0, 0).Add(TimeSpan.FromMinutes(policy.GracePeriodMinutes));
                    if (now > graceEnd)
                    {
                        var lateMinutes = (int)(now - graceEnd).TotalMinutes;
                        record.MarkAsLate(lateMinutes);
                    }
                }
            }
        }

        _context.AttendanceRecords.Add(record);
        await _context.SaveChangesAsync(cancellationToken);

        return record.Id;
    }
}
