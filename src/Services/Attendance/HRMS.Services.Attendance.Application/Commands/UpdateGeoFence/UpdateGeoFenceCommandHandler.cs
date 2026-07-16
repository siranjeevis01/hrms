using HRMS.Services.Attendance.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services.Attendance.Application.Commands.UpdateGeoFence;

public class UpdateGeoFenceCommandHandler : IRequestHandler<UpdateGeoFenceCommand, Unit>
{
    private readonly IAttendanceDbContext _context;

    public UpdateGeoFenceCommandHandler(IAttendanceDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateGeoFenceCommand request, CancellationToken cancellationToken)
    {
        var fence = await _context.GeoFences
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

        if (fence == null)
            throw new InvalidOperationException("Geo-fence not found.");

        fence.Update(
            request.Name,
            request.Latitude,
            request.Longitude,
            request.RadiusMeters,
            request.WorkingDays,
            request.StartTime,
            request.EndTime);

        if (request.IsActive)
            fence.Activate();
        else
            fence.Deactivate();

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
