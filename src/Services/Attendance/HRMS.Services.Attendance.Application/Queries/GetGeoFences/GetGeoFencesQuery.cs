using HRMS.Services.Attendance.Application.DTOs;
using MediatR;

namespace HRMS.Services.Attendance.Application.Queries.GetGeoFences;

public class GetGeoFencesQuery : IRequest<List<GeoFenceDto>>
{
    public Guid CompanyId { get; set; }
}
