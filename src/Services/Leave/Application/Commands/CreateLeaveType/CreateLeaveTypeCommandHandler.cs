using HRMS.Services.Leave.Application.Interfaces;
using HRMS.Services.Leave.Domain.Entities;
using HRMS.Services.Leave.Domain.Enums;
using HRMS.Shared.Kernel.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Leave.Application.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, Guid>
{
    private readonly ILeaveDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateLeaveTypeCommandHandler(ILeaveDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId ?? _currentUserService.TenantId ?? Guid.Empty;

        var existing = await _context.LeaveTypes
            .AnyAsync(lt => lt.Code == request.Code && lt.TenantId == tenantId, cancellationToken);

        if (existing)
            throw new InvalidOperationException($"Leave type with code '{request.Code}' already exists.");

        var gender = Enum.Parse<GenderRestriction>(request.Gender);
        var accrualType = Enum.Parse<AccrualType>(request.AccrualType);

        var id = Guid.NewGuid();
        var leaveType = LeaveType.Create(
            id, request.Name, request.Code, request.Description, request.Color, request.Icon,
            request.IsPaid, request.IsUnlimited, request.DefaultBalanceDays, request.MaxBalanceDays,
            request.MaxCarryForwardDays, request.MaxEncashmentDays, request.CarryForwardExpiryMonths,
            request.AllowEncashment, request.AllowCarryForward, request.AllowHalfDay,
            request.MinDaysPerRequest, request.MaxDaysPerRequest, request.MaxConsecutiveDays,
            request.RequireDocumentation, request.DocumentationDaysThreshold, gender,
            request.ApplicableAfterDays, accrualType, request.AccrualRate, tenantId);

        _context.LeaveTypes.Add(leaveType);
        await _context.SaveChangesAsync(cancellationToken);

        return id;
    }
}
