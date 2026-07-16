using HRMS.Services.Attendance.Application.Interfaces;
using HRMS.Services.Attendance.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Attendance.Application.Commands.UpdateAttendancePolicy;

public class UpdateAttendancePolicyCommandHandler : IRequestHandler<UpdateAttendancePolicyCommand, Unit>
{
    private readonly IAttendanceDbContext _context;

    public UpdateAttendancePolicyCommandHandler(IAttendanceDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateAttendancePolicyCommand request, CancellationToken cancellationToken)
    {
        var policy = await _context.AttendancePolicies
            .FirstOrDefaultAsync(p => p.CompanyId == request.CompanyId, cancellationToken);

        if (policy != null)
        {
            policy.Update(
                request.GracePeriodMinutes,
                request.MaxLateAllowed,
                request.LateDeductionMinutes,
                request.AutoCheckoutTime,
                request.HalfDayMinimumHours,
                request.FullDayMinimumHours,
                request.OvertimeEnabled,
                request.OvertimeThresholdMinutes,
                request.MaxOvertimeMinutes);
        }
        else
        {
            var tenantId = request.TenantId ?? Guid.Empty;

            policy = AttendancePolicy.Create(
                request.CompanyId,
                request.GracePeriodMinutes,
                request.MaxLateAllowed,
                request.LateDeductionMinutes,
                request.AutoCheckoutTime,
                request.HalfDayMinimumHours,
                request.FullDayMinimumHours,
                request.OvertimeEnabled,
                request.OvertimeThresholdMinutes,
                request.MaxOvertimeMinutes,
                tenantId);

            _context.AttendancePolicies.Add(policy);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
