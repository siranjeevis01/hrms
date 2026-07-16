using MediatR;

namespace HRMS.Services.Travel.Application.Commands.SubmitTravelRequest;

public class SubmitTravelRequestCommand : IRequest
{
    public Guid Id { get; set; }
}
