using HRMS.Services.Attendance.Application.Interfaces;
using HRMS.Services.Attendance.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Attendance.Application.Commands.RejectRegularization;

public class RejectRegularizationCommandHandler : IRequestHandler<RejectRegularizationCommand, Unit>
{
    private readonly IAttendanceDbContext _context;

    public RejectRegularizationCommandHandler(IAttendanceDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(RejectRegularizationCommand request, CancellationToken cancellationToken)
    {
        var regularization = await _context.AttendanceRegularizations
            .FirstOrDefaultAsync(r => r.Id == request.RegularizationId, cancellationToken);

        if (regularization == null)
            throw new InvalidOperationException("Regularization request not found.");

        if (regularization.Status != RegularizationStatus.Pending)
            throw new InvalidOperationException("This regularization request has already been processed.");

        regularization.Reject(request.RejectedBy, request.RejectionReason);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
