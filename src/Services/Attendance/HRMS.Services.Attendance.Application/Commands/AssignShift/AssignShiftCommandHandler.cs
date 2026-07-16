using HRMS.Services.Attendance.Application.Interfaces;
using HRMS.Services.Attendance.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Attendance.Application.Commands.AssignShift;

public class AssignShiftCommandHandler : IRequestHandler<AssignShiftCommand, Guid>
{
    private readonly IAttendanceDbContext _context;
    private readonly IAttendanceRepository _repository;

    public AssignShiftCommandHandler(IAttendanceDbContext context, IAttendanceRepository repository)
    {
        _context = context;
        _repository = repository;
    }

    public async Task<Guid> Handle(AssignShiftCommand request, CancellationToken cancellationToken)
    {
        var currentAssignment = await _repository.GetCurrentShiftAssignmentAsync(
            request.EmployeeId, cancellationToken);

        if (currentAssignment != null)
        {
            currentAssignment.EndAssignment(request.EffectiveFrom.AddDays(-1));
        }

        var tenantId = request.TenantId ?? Guid.Empty;

        var assignment = ShiftAssignment.Create(
            request.EmployeeId,
            request.ShiftId,
            request.EffectiveFrom,
            request.EffectiveTo,
            tenantId);

        _context.ShiftAssignments.Add(assignment);
        await _context.SaveChangesAsync(cancellationToken);

        return assignment.Id;
    }
}
