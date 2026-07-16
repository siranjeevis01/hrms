using MediatR;

namespace HRMS.Services.Travel.Application.Commands.ApproveTravelRequest;

public class ApproveTravelRequestCommand : IRequest
{
    public Guid Id { get; set; }
}
