using HRMS.Shared.Kernel.Common;

namespace HRMS.Services.Attendance.Domain.Entities;

public class GeoFence : BaseEntity
{
    public Guid CompanyId { get; private set; }
    public Guid? BranchId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    public double RadiusMeters { get; private set; }
    public bool IsActive { get; private set; }
    public string? WorkingDays { get; private set; }
    public TimeSpan StartTime { get; private set; }
    public TimeSpan EndTime { get; private set; }

    private GeoFence() { }

    public static GeoFence Create(
        Guid companyId,
        Guid? branchId,
        string name,
        double latitude,
        double longitude,
        double radiusMeters,
        string? workingDays,
        TimeSpan startTime,
        TimeSpan endTime,
        Guid? tenantId = null)
    {
        var fence = new GeoFence
        {
            CompanyId = companyId,
            BranchId = branchId,
            Name = name,
            Latitude = latitude,
            Longitude = longitude,
            RadiusMeters = radiusMeters,
            IsActive = true,
            WorkingDays = workingDays,
            StartTime = startTime,
            EndTime = endTime
        };

        if (tenantId.HasValue)
            fence.TenantId = tenantId.Value;

        return fence;
    }

    public void Update(string name, double latitude, double longitude, double radiusMeters, string? workingDays, TimeSpan startTime, TimeSpan endTime)
    {
        Name = name;
        Latitude = latitude;
        Longitude = longitude;
        RadiusMeters = radiusMeters;
        WorkingDays = workingDays;
        StartTime = startTime;
        EndTime = endTime;
    }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;

    public bool IsPointInside(double pointLatitude, double pointLongitude)
    {
        var distance = CalculateDistance(Latitude, Longitude, pointLatitude, pointLongitude);
        return distance <= RadiusMeters;
    }

    public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        const double R = 6371000;
        var dLat = ToRadians(lat2 - lat1);
        var dLon = ToRadians(lon2 - lon1);
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return R * c;
    }

    private static double ToRadians(double degrees) => degrees * Math.PI / 180;
}
