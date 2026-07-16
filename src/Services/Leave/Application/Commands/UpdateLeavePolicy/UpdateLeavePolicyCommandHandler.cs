using HRMS.Services.Leave.Application.Interfaces;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Leave.Application.Commands.UpdateLeavePolicy;

public class UpdateLeavePolicyCommandHandler : IRequestHandler<UpdateLeavePolicyCommand, bool>
{
    private readonly ILeaveDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdateLeavePolicyCommandHandler(ILeaveDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<bool> Handle(UpdateLeavePolicyCommand request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId ?? _currentUserService.TenantId ?? Guid.Empty;

        var policy = await _context.LeavePolicies
            .FirstOrDefaultAsync(p => p.Id == request.Id && p.TenantId == tenantId, cancellationToken)
            ?? throw new InvalidOperationException("Leave policy not found.");

        policy.Update(request.Name, request.Description, request.SandwichPolicyEnabled,
            request.SandwichPolicyDays, request.MinNoticeDays, request.MaxPendingApplications,
            request.AllowBackDatedLeave, request.BackDatedLimitDays);

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
