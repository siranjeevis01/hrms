using HRMS.Services.Attendance.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Attendance.Application.Commands.ApproveWorkFromHome;

public class ApproveWorkFromHomeCommandHandler : IRequestHandler<ApproveWorkFromHomeCommand, Unit>
{
    private readonly IAttendanceDbContext _context;

    public ApproveWorkFromHomeCommandHandler(IAttendanceDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(ApproveWorkFromHomeCommand request, CancellationToken cancellationToken)
    {
        var wfh = await _context.WorkFromHomes
            .FirstOrDefaultAsync(w => w.Id == request.WorkFromHomeId, cancellationToken);

        if (wfh == null)
            throw new InvalidOperationException("Work from home request not found.");

        if (request.IsApproved)
            wfh.Approve(request.ApprovedBy);
        else
            wfh.Reject(request.ApprovedBy);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
