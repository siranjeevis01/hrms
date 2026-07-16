using HRMS.Services.Leave.Application.Interfaces;
using HRMS.Services.Leave.Domain.Entities;
using HRMS.Services.Leave.Domain.Enums;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Leave.Application.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, bool>
{
    private readonly ILeaveDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdateLeaveTypeCommandHandler(ILeaveDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<bool> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId ?? _currentUserService.TenantId ?? Guid.Empty;

        var leaveType = await _context.LeaveTypes
            .FirstOrDefaultAsync(lt => lt.Id == request.Id && lt.TenantId == tenantId, cancellationToken)
            ?? throw new InvalidOperationException("Leave type not found.");

        var gender = Enum.Parse<GenderRestriction>(request.Gender);
        var accrualType = Enum.Parse<AccrualType>(request.AccrualType);

        leaveType.Update(
            request.Name, request.Description, request.Color, request.Icon,
            request.IsPaid, request.MaxBalanceDays, request.MaxCarryForwardDays,
            request.MaxEncashmentDays, request.CarryForwardExpiryMonths,
            request.AllowEncashment, request.AllowCarryForward, request.AllowHalfDay,
            request.MinDaysPerRequest, request.MaxDaysPerRequest, request.MaxConsecutiveDays,
            request.RequireDocumentation, request.DocumentationDaysThreshold, gender,
            request.ApplicableAfterDays, accrualType, request.AccrualRate, request.IsActive);

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
