using HRMS.Services.Attendance.Application.Interfaces;
using HRMS.Services.Attendance.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Attendance.Application.Commands.RequestRegularization;

public class RequestRegularizationCommandHandler : IRequestHandler<RequestRegularizationCommand, Guid>
{
    private readonly IAttendanceDbContext _context;

    public RequestRegularizationCommandHandler(IAttendanceDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(RequestRegularizationCommand request, CancellationToken cancellationToken)
    {
        var record = await _context.AttendanceRecords
            .FirstOrDefaultAsync(a => a.Id == request.AttendanceRecordId, cancellationToken);

        if (record == null)
            throw new InvalidOperationException("Attendance record not found.");

        var tenantId = request.TenantId ?? Guid.Empty;

        var regularization = AttendanceRegularization.Create(
            request.AttendanceRecordId,
            request.EmployeeId,
            request.Reason,
            record.Date,
            record.CheckInTime,
            record.CheckOutTime,
            request.RequestedCheckIn,
            request.RequestedCheckOut,
            tenantId);

        _context.AttendanceRegularizations.Add(regularization);
        await _context.SaveChangesAsync(cancellationToken);

        return regularization.Id;
    }
}
