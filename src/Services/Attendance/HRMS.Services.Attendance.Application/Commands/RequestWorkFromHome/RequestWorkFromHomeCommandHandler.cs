using HRMS.Services.Attendance.Application.Interfaces;
using HRMS.Services.Attendance.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Attendance.Application.Commands.RequestWorkFromHome;

public class RequestWorkFromHomeCommandHandler : IRequestHandler<RequestWorkFromHomeCommand, Guid>
{
    private readonly IAttendanceDbContext _context;

    public RequestWorkFromHomeCommandHandler(IAttendanceDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(RequestWorkFromHomeCommand request, CancellationToken cancellationToken)
    {
        var overlapping = await _context.WorkFromHomes
            .AnyAsync(w =>
                w.EmployeeId == request.EmployeeId &&
                w.StartDate <= request.EndDate &&
                w.EndDate >= request.StartDate,
                cancellationToken);

        if (overlapping)
            throw new InvalidOperationException("A WFH request already exists for the overlapping date range.");

        var tenantId = request.TenantId ?? Guid.Empty;

        var wfh = WorkFromHome.Create(
            request.EmployeeId,
            request.StartDate,
            request.EndDate,
            request.Reason,
            tenantId);

        _context.WorkFromHomes.Add(wfh);
        await _context.SaveChangesAsync(cancellationToken);

        return wfh.Id;
    }
}
