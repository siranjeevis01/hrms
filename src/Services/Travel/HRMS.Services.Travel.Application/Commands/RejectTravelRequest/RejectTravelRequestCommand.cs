using MediatR;

namespace HRMS.Services.Travel.Application.Commands.RejectTravelRequest;

public class RejectTravelRequestCommand : IRequest
{
    public Guid Id { get; set; }
}
