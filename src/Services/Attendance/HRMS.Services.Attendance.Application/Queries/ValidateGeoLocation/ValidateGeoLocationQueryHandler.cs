using HRMS.Services.Attendance.Application.Interfaces;
using MediatR;

namespace HRMS.Services.Attendance.Application.Queries.ValidateGeoLocation;

public class ValidateGeoLocationQueryHandler : IRequestHandler<ValidateGeoLocationQuery, ValidateGeoLocationResult>
{
    private readonly IAttendanceDbContext _context;

    public ValidateGeoLocationQueryHandler(IAttendanceDbContext context)
    {
        _context = context;
    }

    public Task<ValidateGeoLocationResult> Handle(ValidateGeoLocationQuery request, CancellationToken cancellationToken)
    {
        var fences = _context.GeoFences
            .Where(f => f.CompanyId == request.CompanyId && f.IsActive)
            .ToList();

        if (!fences.Any())
        {
            return Task.FromResult(new ValidateGeoLocationResult
            {
                IsValid = false,
                Message = "No active geo-fences configured for this company."
            });
        }

        var now = DateTime.UtcNow.TimeOfDay;
        var today = DateTime.UtcNow.DayOfWeek;

        foreach (var fence in fences)
        {
            if (fence.IsPointInside(request.Latitude, request.Longitude))
            {
                return Task.FromResult(new ValidateGeoLocationResult
                {
                    IsValid = true,
                    Message = $"Inside geo-fence: {fence.Name}",
                    NearestFenceName = fence.Name
                });
            }
        }

        double minDistance = double.MaxValue;
        string? nearestName = null;

        foreach (var fence in fences)
        {
            var distance = Domain.Entities.GeoFence.CalculateDistance(
                fence.Latitude, fence.Longitude,
                request.Latitude, request.Longitude);

            if (distance < minDistance)
            {
                minDistance = distance;
                nearestName = fence.Name;
            }
        }

        return Task.FromResult(new ValidateGeoLocationResult
        {
            IsValid = false,
            Message = "Location is outside all configured geo-fences.",
            NearestFenceDistanceMeters = minDistance,
            NearestFenceName = nearestName
        });
    }
}
