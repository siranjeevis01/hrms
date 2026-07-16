using AutoMapper;
using HRMS.Services.Travel.Application.DTOs;
using HRMS.Services.Travel.Domain.Entities;

namespace HRMS.Services.Travel.Application.Mappings;

public class TravelMappingProfile : Profile
{
    public TravelMappingProfile()
    {
        CreateMap<TravelRequest, TravelRequestDto>();
        CreateMap<TravelItinerary, TravelItineraryDto>();
        CreateMap<TravelExpense, TravelExpenseDto>();
        CreateMap<TravelApproval, TravelApprovalDto>();
        CreateMap<VisaRequest, VisaRequestDto>();
    }
}
