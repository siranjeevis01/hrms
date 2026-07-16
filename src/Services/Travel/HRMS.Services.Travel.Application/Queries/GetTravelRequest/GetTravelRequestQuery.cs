using HRMS.Services.Travel.Application.DTOs;
using MediatR;

namespace HRMS.Services.Travel.Application.Queries.GetTravelRequest;

public class GetTravelRequestQuery : IRequest<TravelRequestDto?>
{
    public Guid Id { get; set; }
}
