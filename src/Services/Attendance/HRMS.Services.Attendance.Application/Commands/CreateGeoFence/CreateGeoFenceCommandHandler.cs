using HRMS.Services.Attendance.Application.Interfaces;
using HRMS.Services.Attendance.Domain.Entities;
using MediatR;

namespace HRMS.Services.Attendance.Application.Commands.CreateGeoFence;

public class CreateGeoFenceCommandHandler : IRequestHandler<CreateGeoFenceCommand, Guid>
{
    private readonly IAttendanceDbContext _context;

    public CreateGeoFenceCommandHandler(IAttendanceDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateGeoFenceCommand request, CancellationToken cancellationToken)
    {
        var tenantId = request.TenantId ?? Guid.Empty;

        var fence = GeoFence.Create(
            request.CompanyId,
            request.BranchId,
            request.Name,
            request.Latitude,
            request.Longitude,
            request.RadiusMeters,
            request.WorkingDays,
            request.StartTime,
            request.EndTime,
            tenantId);

        _context.GeoFences.Add(fence);
        await _context.SaveChangesAsync(cancellationToken);

        return fence.Id;
    }
}
