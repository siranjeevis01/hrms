using HRMS.Services.Travel.Application.DTOs;
using MediatR;

namespace HRMS.Services.Travel.Application.Queries.GetItinerary;

public class GetItineraryQuery : IRequest<List<TravelItineraryDto>>
{
    public Guid TravelRequestId { get; set; }
}
