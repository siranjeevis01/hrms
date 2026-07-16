using HRMS.Services.Travel.Application.DTOs;
using HRMS.Services.Travel.Domain.Enums;
using MediatR;

namespace HRMS.Services.Travel.Application.Queries.GetTravelRequests;

public class GetTravelRequestsQuery : IRequest<PagedResult<TravelRequestDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Guid? EmployeeId { get; set; }
    public TravelRequestStatus? Status { get; set; }
    public string? Destination { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string? SearchTerm { get; set; }
}
