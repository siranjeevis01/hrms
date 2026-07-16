using HRMS.Services.Leave.Application.Interfaces;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Leave.Application.Commands.UseCompOff;

public class UseCompOffCommandHandler : IRequestHandler<UseCompOffCommand, bool>
{
    private readonly ILeaveDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UseCompOffCommandHandler(ILeaveDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<bool> Handle(UseCompOffCommand request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId ?? _currentUserService.TenantId ?? Guid.Empty;

        var compOff = await _context.CompOffs
            .FirstOrDefaultAsync(co => co.Id == request.CompOffId && co.TenantId == tenantId, cancellationToken)
            ?? throw new InvalidOperationException("Comp-off not found.");

        if (compOff.EmployeeId != request.EmployeeId)
            throw new InvalidOperationException("You can only use your own comp-offs.");

        compOff.Use();
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
