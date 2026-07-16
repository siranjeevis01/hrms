using HRMS.Services.Attendance.Application.Interfaces;
using HRMS.Services.Attendance.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Attendance.Application.Commands.ApproveRegularization;

public class ApproveRegularizationCommandHandler : IRequestHandler<ApproveRegularizationCommand, Unit>
{
    private readonly IAttendanceDbContext _context;

    public ApproveRegularizationCommandHandler(IAttendanceDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(ApproveRegularizationCommand request, CancellationToken cancellationToken)
    {
        var regularization = await _context.AttendanceRegularizations
            .FirstOrDefaultAsync(r => r.Id == request.RegularizationId, cancellationToken);

        if (regularization == null)
            throw new InvalidOperationException("Regularization request not found.");

        if (regularization.Status != RegularizationStatus.Pending)
            throw new InvalidOperationException("This regularization request has already been processed.");

        regularization.Approve(request.ApprovedBy);

        var record = await _context.AttendanceRecords
            .FirstOrDefaultAsync(a => a.Id == regularization.AttendanceRecordId, cancellationToken);

        if (record != null)
        {
            if (regularization.RequestedCheckIn.HasValue)
                record.CheckOut(Domain.Enums.CheckInMethod.Manual);

            if (regularization.RequestedCheckOut.HasValue)
                record.CheckOut(Domain.Enums.CheckInMethod.Manual);

            record.Approve(request.ApprovedBy);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
