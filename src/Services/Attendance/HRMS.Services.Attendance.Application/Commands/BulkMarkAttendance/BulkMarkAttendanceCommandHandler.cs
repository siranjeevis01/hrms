using HRMS.Services.Attendance.Application.Interfaces;
using HRMS.Services.Attendance.Domain.Entities;
using HRMS.Services.Attendance.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Attendance.Application.Commands.BulkMarkAttendance;

public class BulkMarkAttendanceCommandHandler : IRequestHandler<BulkMarkAttendanceCommand, int>
{
    private readonly IAttendanceDbContext _context;
    private readonly IAttendanceRepository _repository;

    public BulkMarkAttendanceCommandHandler(IAttendanceDbContext context, IAttendanceRepository repository)
    {
        _context = context;
        _repository = repository;
    }

    public async Task<int> Handle(BulkMarkAttendanceCommand request, CancellationToken cancellationToken)
    {
        var recordsCreated = 0;
        var tenantId = request.TenantId ?? Guid.Empty;

        foreach (var entry in request.Entries)
        {
            var existingRecord = await _repository.GetByEmployeeAndDateAsync(
                entry.EmployeeId, entry.Date.Date, cancellationToken);

            if (existingRecord != null)
            {
                existingRecord.UpdateStatus(entry.Status);
                if (entry.Notes != null)
                    existingRecord.UpdateNotes(entry.Notes);
            }
            else
            {
                var record = AttendanceRecord.CreateCheckIn(
                    entry.EmployeeId,
                    entry.Date,
                    null,
                    CheckInMethod.Manual,
                    tenantId: tenantId);

                record.UpdateStatus(entry.Status);

                if (entry.Notes != null)
                    record.UpdateNotes(entry.Notes);

                _context.AttendanceRecords.Add(record);
                recordsCreated++;
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return recordsCreated;
    }
}
