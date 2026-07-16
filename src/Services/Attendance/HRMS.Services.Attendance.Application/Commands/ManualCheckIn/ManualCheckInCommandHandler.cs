using HRMS.Services.Attendance.Application.Interfaces;
using HRMS.Services.Attendance.Domain.Entities;
using HRMS.Services.Attendance.Domain.Enums;
using MediatR;

namespace HRMS.Services.Attendance.Application.Commands.ManualCheckIn;

public class ManualCheckInCommandHandler : IRequestHandler<ManualCheckInCommand, Guid>
{
    private readonly IAttendanceDbContext _context;
    private readonly IAttendanceRepository _repository;

    public ManualCheckInCommandHandler(IAttendanceDbContext context, IAttendanceRepository repository)
    {
        _context = context;
        _repository = repository;
    }

    public async Task<Guid> Handle(ManualCheckInCommand request, CancellationToken cancellationToken)
    {
        var existingRecord = await _repository.GetByEmployeeAndDateAsync(
            request.EmployeeId, request.Date.Date, cancellationToken);

        if (existingRecord != null)
            throw new InvalidOperationException($"An attendance record already exists for {request.Date:yyyy-MM-dd}.");

        var tenantId = request.TenantId ?? Guid.Empty;

        var record = AttendanceRecord.CreateCheckIn(
            request.EmployeeId,
            request.Date,
            request.ShiftId,
            CheckInMethod.Manual,
            tenantId: tenantId);

        record.UpdateStatus(request.Status);

        if (request.CheckInTime.HasValue)
            record.UpdateNotes($"Admin set check-in to {request.CheckInTime:HH:mm}");

        if (request.CheckOutTime.HasValue)
            record.CheckOut(CheckInMethod.Manual);

        if (request.Notes != null)
            record.UpdateNotes(request.Notes);

        _context.AttendanceRecords.Add(record);
        await _context.SaveChangesAsync(cancellationToken);

        return record.Id;
    }
}
