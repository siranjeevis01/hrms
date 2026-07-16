using MediatR;

namespace HRMS.Services.Travel.Application.Commands.CancelTravelRequest;

public class CancelTravelRequestCommand : IRequest
{
    public Guid Id { get; set; }
}
