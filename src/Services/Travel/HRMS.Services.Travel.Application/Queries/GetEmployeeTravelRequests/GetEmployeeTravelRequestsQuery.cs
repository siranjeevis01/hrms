using HRMS.Services.Travel.Application.DTOs;
using HRMS.Services.Travel.Application.Queries.GetTravelRequests;
using HRMS.Services.Travel.Domain.Enums;
using MediatR;

namespace HRMS.Services.Travel.Application.Queries.GetEmployeeTravelRequests;

public class GetEmployeeTravelRequestsQuery : IRequest<PagedResult<TravelRequestDto>>
{
    public Guid EmployeeId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public TravelRequestStatus? Status { get; set; }
}
