using HRMS.Services.Leave.Application.Interfaces;
using HRMS.Services.Leave.Domain.Entities;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Leave.Application.Commands.EarnCompOff;

public class EarnCompOffCommandHandler : IRequestHandler<EarnCompOffCommand, Guid>
{
    private readonly ILeaveDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public EarnCompOffCommandHandler(ILeaveDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(EarnCompOffCommand request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId ?? _currentUserService.TenantId ?? Guid.Empty;

        var id = Guid.NewGuid();
        var expiryDate = request.EarnedDate.AddMonths(3);

        var compOff = CompOff.Create(
            id,
            request.EmployeeId,
            request.LeaveApplicationId,
            request.EarnedDate,
            expiryDate,
            request.Days,
            request.Reason,
            tenantId);

        _context.CompOffs.Add(compOff);
        await _context.SaveChangesAsync(cancellationToken);

        return id;
    }
}
