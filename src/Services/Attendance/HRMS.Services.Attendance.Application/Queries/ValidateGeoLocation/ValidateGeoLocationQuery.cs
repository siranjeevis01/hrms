using MediatR;

namespace HRMS.Services.Attendance.Application.Queries.ValidateGeoLocation;

public class ValidateGeoLocationQuery : IRequest<ValidateGeoLocationResult>
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public Guid CompanyId { get; set; }
}

public class ValidateGeoLocationResult
{
    public bool IsValid { get; set; }
    public string? Message { get; set; }
    public double? NearestFenceDistanceMeters { get; set; }
    public string? NearestFenceName { get; set; }
}
